using viecLam24hBE.Models;

namespace viecLam24hBE.Services
{
    public interface ApplicantProfileService
    {
        ApplicantProfile GetApplicantProfileById(int id);

        List<ApplicantProfile> GetApplicantProfiles();

        Task UpdateApplicantProfiles(ApplicantProfile applicantProfile);
    }
}
