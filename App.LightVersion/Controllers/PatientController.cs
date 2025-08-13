#nullable enable
using App.LightVersion.Models.Contracts.Incoming;
using App.LightVersion.Models.Entities;
using App.LightVersion.Models.Enums;
using App.LightVersion.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace App.LightVersion.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class PatientController : ControllerBase
{
    private readonly IPatientService _patientService;


    private readonly Regex _acceptFilterTypes = new(@"^(eq|ne|gt|lt|ge|le|ap)?(.*)$");

    public PatientController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePatientRequest request)
    {
        try
        {
            var name = new Name(
                request.Name.Family,
                request.Name.Given,
                request.Name.Use,
                request.Name.Id
            );

            var genderEnum = ParseGender(request.Gender);

            var patient = await _patientService.CreatePatientAsync(
                name,
                request.BirthDate,
                genderEnum,
                request.Active
            );

            return CreatedAtAction(nameof(GetById), new { id = patient.Id }, patient);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var patient = await _patientService.GetPatientByIdAsync(id);
        return patient is null ? NotFound() : Ok(patient);
    }

    private GenderType ParseGender(string gender)
    {
        return gender.ToLowerInvariant() switch
        {
            "male" => GenderType.Male,
            "female" => GenderType.Female,
            "other" => GenderType.Other,
            "unknown" => GenderType.Unknown,
            _ => GenderType.Unknown
        };
    }
}