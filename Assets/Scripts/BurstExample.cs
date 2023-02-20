using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public class BurstExample : MonoBehaviour
{
    void Start()
    {
        var input = new NativeArray<float>(10, Allocator.Persistent);
        var output = new NativeArray<float>(1, Allocator.Persistent);

        for (var i = 0; i < input.Length; i++)
        {
            input[i] = i;
        }

        var job = new Job { input = input, output = output };
        job.Schedule().Complete();
        Debug.Log("result: " + output[0]);

        input.Dispose();
        output.Dispose();
    }

    [BurstCompile]
    private struct Job : IJob
    {
        [ReadOnly]
        public NativeArray<float> input;

        [WriteOnly]
        public NativeArray<float> output;

        public void Execute()
        {
            var result = 0.0f;
            for (var i = 0; i < input.Length; i++)
            {
                result += input[i];
            }

            output[0] = result;
        }
    }
}
