using System.Collections.Generic;

namespace SerializedTuples.Runtime
{
    public static class SerializedTupleExtensions
    {
        public static void Deconstruct<T1>(this SerializedTuple<T1> value, out T1 item1)
        {
            item1 = value.v1;
        }

        public static void Deconstruct<T1, T2>(
            this SerializedTuple<T1, T2> value,
            out T1 item1,
            out T2 item2
        )
        {
            item1 = value.v1;
            item2 = value.v2;
        }

        #region PRIVATE_SERIALIZED_VARS
        #endregion


        #region PRIVATE_VARS
        #endregion


        #region PRIVATE_PROPERTIES
        #endregion


        #region PUBLIC_VARS
        #endregion


        #region PUBLIC_PROPERTIES
        #endregion


        #region UNITY_CALLBAKCS
        #endregion


        #region CONSTRUCTOR
        #endregion


        #region PRIVATE_METHODS
        #endregion


        #region PROTECTED_METHODS
        #endregion


        #region PUBLIC_METHODS
        public static Dictionary<T1, T2> ToDictionary<T1, T2>(
            this SerializedTuple<T1, T2>[] array,
            bool suppressException = false
        )
        {
            Dictionary<T1, T2> lookup = new Dictionary<T1, T2>();

            for (int i = 0; i < array.Length; i++)
            {
                if (lookup.ContainsKey(array[i].v1))
                {
                    if (suppressException)
                        throw new System.Exception(
                            $"Trying to insert {array[i].v2} while key {array[i].v1} is already present"
                        );
                    else
                        continue;
                }

                lookup.Add(array[i].v1, array[i].v2);
            }

            return lookup;
        }

        #endregion


        #region EVENT_CALLBACKS
        #endregion


        #region COROUTINES
        #endregion

        #region UI_CALLBACKS
        #endregion

        #region INNERCLASS_DEFINITIONS
        #endregion


        #region EDITOR_ACCESSORS_OR_HELPERS
#if UNITY_EDITOR

#endif
        #endregion
    }
}
