
namespace Garitto.Extensions
{
    static class ArrayExtension
    {
        public static T Random<T>(this T[] array)
        {
            return array[array.RandomIndex()];
        }

        public static int RandomIndex<T>(this T[] array)
        {
            return UnityEngine.Random.Range(0, array.Length);
        }
    }
}
