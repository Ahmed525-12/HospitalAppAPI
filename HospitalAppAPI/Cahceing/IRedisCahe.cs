namespace HospitalAppAPI.Cahceing
{
    public interface IRedisCahe
    {
        T? GetData<T>(string key);

        void SetData<T>(string key, T data);
    }
}