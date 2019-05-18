namespace Realtime.Presenter.Function.Credentials.Dtos
{
    public class CredentialsDto
    {
        public string SignalRUrl { get; }
        public string SignalRToken { get; }

        public CredentialsDto(string signalRUrl, string signalRToken)
        {
            SignalRUrl = signalRUrl;
            SignalRToken = signalRToken;
        }
    }
}