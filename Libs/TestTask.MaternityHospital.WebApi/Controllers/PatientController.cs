#nullable enable
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TestTask.MaternityHospital.Domain.Interfaces.Common.Exceptions;
using TestTask.MaternityHospital.Domain.Interfaces.Patients.Exceptions;
using TestTask.MaternityHospital.Domain.Interfaces.Patients.Manages;
using TestTask.MaternityHospital.Domain.Interfaces.Patients.Models;
using TestTask.MaternityHospital.FHIR.Models.Implementations;
using TestTask.MaternityHospital.WebApi.Contracts.Patients.Incoming;
using TestTask.MaternityHospital.WebApi.Contracts.Patients.Outgoing;
using TestTask.MaternityHospital.WebApi.Extensions;
using TestTask.MaternityHospital.WebApi.Helpers;

namespace TestTask.MaternityHospital.WebApi.Controllers;

public sealed class PatientController : BaseController
{
    private readonly IPatientManager _patientManager;


    private readonly Regex _acceptFilterTypes = new(@"^(eq|ne|gt|lt|ge|le|ap)?(.*)$");

    public PatientController(IPatientManager patientManager)
    {
        _patientManager = patientManager;
    }

    /// <summary>
    /// Создание пациента 
    /// </summary>
    /// <param name="incContract"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("api/patient")]
    public async Task<ActionResult> CreatePatient([FromBody] PatientIncContract incContract)
    {
        var patient = _patientManager.CreatePatient(incContract.Name.Family, incContract.BirthDate);

        var (givenFirst, givenSecond) = incContract.Name!.Given.ExtractTwoGivenNames();

        var genderType = GenderTypeConverter.Convert(incContract.Gender);
        patient.Configure(incContract.Name?.Use, givenFirst, givenSecond, genderType, incContract.Active);
        await patient.Save();

        var result = new PatientOutContract(patient);
        return Ok(result);
    }


    /// <summary>
    /// Поиск пациента по уникальному ключу
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("api/patient/{id}")]
    public async Task<ActionResult> GetPatient(string id)
    {
        try
        {
            var patient = await _patientManager.GetPatient(id);

            var result = new PatientOutContract(patient);
            return Ok(result);
        }
        catch (InstanceNotFoundException<IPatient> ex)
        {
            return UnprocessableEntity(ex.Message);
        }
    }

    /// <summary>
    /// Поиск пациентов по диапазону дат рождения. Отсутствие фильтров - вывод всех пациентов.
    /// </summary>
    /// <param name="birthdate"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("api/patients/searchByBirthDate")]
    public async Task<ActionResult> SearchPatientsByDate([FromQuery] List<string> birthdate)
    {
        var filters = ParseBirthDateFilters(birthdate);
        var patients = await _patientManager.SearchPatientsByBirthDateAsync(filters);

        var result = patients.Select(p => new PatientOutContract(p));

        return Ok(result);
    }


    /// <summary>
    /// Метод обновления сущности пациента
    /// </summary>
    /// <param name="id"></param>
    /// <param name="incContract"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("api/patient/{id}")]
    public async Task<ActionResult> UpdatePatient(string id, [FromBody] PatientIncContract incContract)
    {
        try
        {
            var patient = await _patientManager.GetPatient(id);

            patient.Family = incContract.Name.Family;
            patient.BirthDateTimeUtc = incContract.BirthDate;

            var (givenFirst, givenSecond) = incContract.Name!.Given.ExtractTwoGivenNames();

            var genderType = GenderTypeConverter.Convert(incContract.Gender);
            patient.Configure(incContract.Name.Use, givenFirst, givenSecond, genderType, incContract.Active);
            await patient.Save();

            var result = new PatientOutContract(patient);

            return Ok(result);
        }
        catch (InstanceNotFoundException<IPatient> ex)
        {
            return UnprocessableEntity(ex.Message);
        }
        catch (FamilyNotValidException ex)
        {
            return UnprocessableEntity(ex.Message);
        }
        catch (BirthDateTimeUtcNotValidException ex)
        {
            return UnprocessableEntity(ex.Message);
        }
    }



    /// <summary>
    /// Удалить пациента по уникальному ключу
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("api/patient/{id}")]
    public async Task<ActionResult> DeletePatient(string id)
    {
        try
        {
            var patient = await _patientManager.GetPatient(id);
            await patient.Delete();

            return Ok();
        }
        catch (InstanceNotFoundException<IPatient> ex)
        {
            return UnprocessableEntity(ex.Message);
        }
    }

    #region PrivateMethods

    private List<BirthDateSearchFilter> ParseBirthDateFilters(List<string> incDates)
    {
        var filters = new List<BirthDateSearchFilter>();
        foreach (var incDate in incDates)
        {
            var match = _acceptFilterTypes.Match(incDate);
            if (!match.Success)
                continue;

            var prefix = match.Groups[1].Success ? match.Groups[1].Value.ToLower() : "eq";
            var dateStr = match.Groups[2].Value;

            if (!DateTime.TryParse(dateStr, out var date))
                continue;

            var rule = FilterRuleConverter.Convert(prefix);

            filters.Add(new BirthDateSearchFilter(rule, date));
        }
        return filters;
    }

    #endregion

}