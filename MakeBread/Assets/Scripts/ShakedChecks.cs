using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Shakedの判定受け取りに対する状態の列挙
/// </summary>
public enum ShakedStatus
{
    /// <summary>
    /// 振った判定の受け取りを許可するとき
    /// </summary>
    GetShaked,

    /// <summary>
    /// 振った判定の受け取りを一時停止するとき
    /// </summary>
    StopShaked,

    /// <summary>
    /// 振った判定を受け取らないとき
    /// </summary>
    Off
}

public class ShakedChecks : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
