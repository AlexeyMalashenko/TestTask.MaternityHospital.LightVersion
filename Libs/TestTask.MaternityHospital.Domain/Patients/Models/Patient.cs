using System;
using System.Threading.Tasks;
using TestTask.MaternityHospital.Domain.Interfaces.Common.Exceptions;
using TestTask.MaternityHospital.Domain.Interfaces.Patients.Exceptions;
using TestTask.MaternityHospital.Domain.Interfaces.Patients.Models;
using TestTask.MaternityHospital.Domain.Patients.Helpers;
using TestTask.MaternityHospital.StorageService.Interfaces.Patients.Enums;
using TestTask.MaternityHospital.StorageService.Interfaces.Patients.Exceptions;
using TestTask.MaternityHospital.StorageService.Interfaces.Patients.Models;
using TestTask.MaternityHospital.StorageService.Interfaces.Patients.Providers;

namespace TestTask.MaternityHospital.Domain.Patients.Models;

internal sealed class Patient : IPatient
{
    private readonly IPatientDataProvider _patientDataProvider;

    private string _family;
    private DateTime _birthDateTimeUtc;
    private bool _isChanged;


    private bool _isNew;

    public string Use { get; private set; }
    public string GivenFirst { get; private set; }
    public string GivenSecond { get; private set; }
    public GenderType Gender { get; private set; }


    public string PatientGlobalKey { get; }

    public string Family
    {
        get => _family;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new FamilyNotValidException();
            }

            _family = value;
            _isChanged = true;
        }
    }


    public DateTime BirthDateTimeUtc
    {
        get => _birthDateTimeUtc;
        set
        {
            if (DateTime.MinValue == value)
            {
                throw new BirthDateTimeUtcNotValidException();
            }

            _birthDateTimeUtc = value;
            _isChanged = true;
        }
    }

    public bool IsActive { get; private set; }


    public Patient(string family, DateTime birthDateTimeUtc, IPatientDataProvider patientDataProvider)
    {
        PatientGlobalKey = Guid.NewGuid().ToString();
        Family = family;
        BirthDateTimeUtc = birthDateTimeUtc;

        _patientDataProvider = patientDataProvider;
        _isNew = true;
    }

    public Patient(PatientDataModel dataModel, IPatientDataProvider patientDataProvider)
    {
        PatientGlobalKey = dataModel.PatientGlobalKey;
        Family = dataModel.Family;
        BirthDateTimeUtc = dataModel.BirthDateTimeUtc;
        GivenFirst = dataModel.GivenFirst;
        GivenSecond = dataModel.GivenSecond;
        Use = dataModel.Use;
        Gender = GenderTypeConverter.Convert(dataModel.Gender);

        IsActive = dataModel.IsActive;

        _patientDataProvider = patientDataProvider;
    }



    public void Configure(string use, string givenFirst, string givenSecond, GenderType gender, bool isActive)
    {
        Use = use;
        GivenFirst = givenFirst;
        GivenSecond = givenSecond;
        Gender = gender;
        IsActive = isActive;

        _isChanged = true;
    }


    public async Task Save()
    {
        try
        {
            if (_isNew)
            {
                var dataModel = ConvertToDataModel();
                await _patientDataProvider.CreatePatient(dataModel);
                _isNew = false;
            }
            else if (_isChanged)
            {
                var dataModel = ConvertToDataModel();
                await _patientDataProvider.UpdatePatient(dataModel);
            }

        }
        catch (PatientNotFoundProviderException)
        {
            throw new InstanceNotFoundException<IPatient>(nameof(PatientGlobalKey), PatientGlobalKey);
        }
    }

    public async Task Delete()
    {
        await _patientDataProvider.DeletePatient(PatientGlobalKey);
    }

    private PatientDataModel ConvertToDataModel()
    {
        var result = new PatientDataModel
        {
            PatientGlobalKey = PatientGlobalKey,
            Use = Use,
            Family = Family,
            GivenFirst = GivenFirst,
            GivenSecond = GivenSecond,
            Gender = GenderTypeConverter.Convert(Gender),
            BirthDateTimeUtc = BirthDateTimeUtc,
            IsActive = IsActive
        };
        return result;
    }
}