namespace HematosBO
{
    public interface ILuminexData
    {
        int LumID { get; set; }

        string SerialNumber { get; set; }

        string LuminexID { get; set; }

        string LuminexServer { get; set; }
    }
}