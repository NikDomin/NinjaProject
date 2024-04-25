using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

namespace Services
{
    public class Authentication : MonoBehaviour
    {
        private void Start()
        {
            if (!Social.localUser.authenticated)
            {
                PlayGamesPlatform.DebugLogEnabled = true;
                PlayGamesPlatform.Activate();
                PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
            }
            
        }
        internal void ProcessAuthentication(SignInStatus status)
        {
            if (status == SignInStatus.Success)
            {
                // Continue with Play Games Services
                string name = PlayGamesPlatform.Instance.GetUserDisplayName();
            }
            else
            {
                // Disable your integration with Play Games Services or show a login button
                // to ask users to sign-in. Clicking it should call
                // PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication).
            }
        }
    }
}