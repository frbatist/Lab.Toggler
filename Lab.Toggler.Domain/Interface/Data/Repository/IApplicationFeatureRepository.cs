﻿using Lab.Toggler.Domain.Entities;
using System.Threading.Tasks;

namespace Lab.Toggler.Domain.Interface.Data.Repository
{
    public interface IApplicationFeatureRepository : IRepository<ApplicationFeature>
    {
        Task<ApplicationFeature> GetAsync(int applicationId, int featureId);
        Task<ApplicationFeature> GetApplicationFeatureAsync(int id);
    }
}
