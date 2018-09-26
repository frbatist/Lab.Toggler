using System.Threading.Tasks;
using Lab.Toggler.Model;

namespace Lab.Toggler.ApplicationService
{
    public interface IFeatureAppService
    {
        Task<FeatureModelResponse> Add(FeatureModel featureModel);
        Task<ApplicationFeatureResponseModel> AddApplicationFeature(ApplicationFeatureModel applicationFeatureModel);
        Task ToggleApplicationFeature(ApplicationFeatureModel applicationFeatureModel);
        Task ToggleFeature(FeatureModel featureModel);
        Task<FeatureCheckModelResponse> Check(string application, string version, string featureName);
    }
}