using UnityEngine;

/// <summary>
/// Time utility class to create a game-specific clock, separate from the regular engine clock.
/// This allows for grouping actors by clock as pausing this clock's time scale won't pause the general time scale.
/// Making it non-static can allow for multiple instances with different groupings to be created.
/// </summary>
public static class TimeUtil
{
    private static float previousTime;
    public static float deltaTime;
    public static float timeScale;

    /// <summary>
    /// Initializes this sub-clock class
    /// </summary>
    public static void Initialize()
    {
        previousTime = Time.time;
        timeScale = Time.timeScale;
        deltaTime = 0.0f;
    }

    /// <summary>
    /// Acts as Unity's delta time update, call it once per frame somewhere;
    /// </summary>
    public static void UpdateDeltaTime()
    {
        deltaTime = Time.time - previousTime;
        previousTime = Time.time;
    }

    /// <summary>
    /// Acts as Unity's Time.deltaTime;
    /// </summary>
    /// <returns>Returns the time delta modified by the active time scale;</returns>
    public static float GetDelta()
    {

        return deltaTime * timeScale;
    }
}