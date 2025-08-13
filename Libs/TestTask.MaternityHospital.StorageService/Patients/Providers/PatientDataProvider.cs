//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Threading.Tasks;
//using TestTask.MaternityHospital.FHIR.Enums;
//using TestTask.MaternityHospital.FHIR.Models.Implementations;
//using TestTask.MaternityHospital.StorageService.Interfaces.Patients.Exceptions;
//using TestTask.MaternityHospital.StorageService.Interfaces.Patients.Models;
//using TestTask.MaternityHospital.StorageService.Interfaces.Patients.Providers;
//using TestTask.MaternityHospital.StorageService.Persistence;

//namespace TestTask.MaternityHospital.StorageService.Patients.Providers;

//public sealed class PatientDataProvider : IPatientDataProvider
//{
//    private readonly PatientsDbContext _context;

//    public PatientDataProvider(PatientsDbContext context)
//    {
//        _context = context;
//    }

//    public async Task CreatePatient(PatientDataModel dataModel)
//    {
//        _context.Patients.Add(dataModel);
//        await _context.SaveChangesAsync();
//    }

//    public async Task<PatientDataModel> GetPatient(string patientGlobalKey)
//    {
//        return await _context.Patients.FirstOrDefaultAsync(p => p.PatientGlobalKey == patientGlobalKey);
//    }

//    public async Task<List<PatientDataModel>> SearchPatientsByBirthDate(List<BirthDateSearchFilter> filters)
//    {
//        var query = _context.Patients.AsQueryable();

//        foreach (var filter in filters)
//        {
//            var predicate = BuildFilterExpression(filter);
//            query = query.Where(predicate);
//        }

//        return await query.ToListAsync();
//    }

//    public async Task UpdatePatient(PatientDataModel dataModel)
//    {
//        var existingPatient = await _context.Patients
//            .FirstOrDefaultAsync(p => p.PatientGlobalKey == dataModel.PatientGlobalKey);

//        if (existingPatient == null)
//        {
//            throw new PatientNotFoundProviderException();
//        }

//        existingPatient.Use = dataModel.Use;
//        existingPatient.Family = dataModel.Family;
//        existingPatient.GivenFirst = dataModel.GivenFirst;
//        existingPatient.GivenSecond = dataModel.GivenSecond;
//        existingPatient.Gender = dataModel.Gender;
//        existingPatient.BirthDateTimeUtc = dataModel.BirthDateTimeUtc;
//        existingPatient.IsActive = dataModel.IsActive;

//        await _context.SaveChangesAsync();
//    }

//    public async Task DeletePatient(string patientGlobalKey)
//    {
//        var entity = await _context.Patients
//            .FirstOrDefaultAsync(p => p.PatientGlobalKey == patientGlobalKey);

//        if (entity is not null)
//        {
//            _context.Patients.Remove(entity);
//            await _context.SaveChangesAsync();
//        }
//    }


//    #region #PrivateMetods
//    private static Expression<Func<PatientDataModel, bool>> BuildFilterExpression(BirthDateSearchFilter filter)
//    {
//        return filter.Operator switch
//        {
//            FilterRule.Equal => p => p.BirthDateTimeUtc.Date == filter.Date.Date,
//            FilterRule.NotEqual => p => p.BirthDateTimeUtc.Date != filter.Date.Date,
//            FilterRule.GreaterThan => p => p.BirthDateTimeUtc.Date > filter.Date.Date,
//            FilterRule.LessThan => p => p.BirthDateTimeUtc.Date < filter.Date.Date,
//            FilterRule.GreaterOrEqual => p => p.BirthDateTimeUtc.Date >= filter.Date.Date,
//            FilterRule.LessOrEqual => p => p.BirthDateTimeUtc.Date <= filter.Date.Date,
//            FilterRule.StartsAfter => p => p.BirthDateTimeUtc > filter.Date,
//            FilterRule.EndsBefore => p => p.BirthDateTimeUtc < filter.Date,
//            FilterRule.Approximately => p => p.BirthDateTimeUtc.Date >= filter.Date.AddDays(-1).Date &&
//                                             p.BirthDateTimeUtc.Date <= filter.Date.AddDays(1).Date,
//            _ => p => true
//        };
//    }

//    #endregion
//}