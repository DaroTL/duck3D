using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;

namespace Duck.three_dimension_menagment
{
    public class Math3D
    {
        public static readonly int forSort = 10;
        public static Vector3 normalizeVector(Vector3 vec)
        {

            return new Vector3(

                vec.x > 0? 
                1: 
                vec.x != 0? 
                -1: 0,
                
                vec.y > 0? 
                1: 
                vec.y != 0? 
                -1: 0,

                vec.z > 0 ?
                1 :
                vec.z != 0 ?
                -1 : 0

                );
        }

        public static void insertionSort(ref Triangle3[] arr, int left, int right)
        {
            for (int i = left + 1; i <= right; i++)
            {
                float zi = (float)((arr[i].p1.global.z + arr[i].p2.global.z + arr[i].p3.global.z) / 3);
                Triangle3 temp = arr[i];
                int j = i - 1;
                double zj = (arr[j].p1.global.z + arr[j].p2.global.z + arr[j].p3.global.z) / 3;
                double zj1 = (arr[j + 1].p1.global.z + arr[j + 1].p2.global.z + arr[j + 1].p3.global.z) / 3;
                while (j >= left && zj > zi)
                {
                    zj = (arr[j].p1.global.z + arr[j].p2.global.z + arr[j].p3.global.z) / 3;
                    arr[j + 1] = arr[j];
                    j--;
                }
                arr[j + 1] = temp;
            }
        }

        public static void merge(ref Triangle3[] arr, int l, int m, int r)
        {

            int len1 = m - l + 1, len2 = r - m;
            Triangle3[] left = new Triangle3[len1];
            Triangle3[] right = new Triangle3[len2];
            for (int x = 0; x < len1; x++)
                left[x] = arr[l + x];
            for (int x = 0; x < len2; x++)
                right[x] = arr[m + 1 + x];

            int i = 0;
            int j = 0;
            int k = l;


            while (i < len1 && j < len2)
            {
                if (((left[i].p1.global.z + left[i].p2.global.z + left[i].p3.global.z) / 3) <= ((right[j].p1.global.z + right[j].p2.global.z + right[j].p3.global.z) / 3))
                {
                    arr[k] = left[i];
                    i++;
                }
                else
                {
                    arr[k] = right[j];
                    j++;
                }
                k++;
            }


            while (i < len1)
            {
                arr[k] = left[i];
                k++;
                i++;
            }


            while (j < len2)
            {
                arr[k] = right[j];
                k++;
                j++;
            }

        }


        public static void timSort(ref Triangle3[] arr, int n)
        {


            for (int i = 0; i < n; i += forSort)
                insertionSort(ref arr, i,
                             Math.Min((i + forSort - 1), (n - 1)));


            for (int size = forSort; size < n;
                                     size = 2 * size)
            {

                for (int left = 0; left < n; left += 2 * size)
                {

                    int mid = left + size - 1;
                    int right = Math.Min((left +
                                        2 * size - 1), (n - 1));


                    if (mid < right)
                        merge(ref arr, left, mid, right);
                }
            }
        }

    }
}
