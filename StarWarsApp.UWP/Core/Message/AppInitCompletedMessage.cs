namespace Core.Message
{
    public class AppInitCompletedMessage
    {
        public bool ResumingFromTerminated { get; set; }

        public AppInitCompletedMessage(bool resumingFromTerminated = false)
        {
            ResumingFromTerminated = resumingFromTerminated;
        }
    }
}