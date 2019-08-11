using SignUp.core.ValueObjects;

using System.Threading.Tasks;

namespace SignUp.core.Ports
{
    public interface ISignUpNotifier
    {
        Task NotifyOfRegistrationOutcome(Student student, Course course, bool wasRegistrationSuccess);
    }
}
