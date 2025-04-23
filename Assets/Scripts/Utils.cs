// TMPReactiveExtensions.cs
using System;
using UniRx;
using TMPro;

public static class TMPReactiveExtensions
{
    /// <summary>
    /// IObservable<T> 시퀀스의 값을 ToString() 하여
    /// TMP_Text.text 에 자동으로 바인딩해 줍니다.
    /// </summary>
    public static IDisposable SubscribeToTextMeshPro<T>(
        this IObservable<T> source,
        TMP_Text tmpText)
    {
        return source.Subscribe(x => tmpText.text = x?.ToString());
    }

    /// <summary>
    /// 포맷을 직접 지정하고 싶을 때 사용하세요.
    /// </summary>
    public static IDisposable SubscribeToTextMeshPro<T>(
        this IObservable<T> source,
        TMP_Text tmpText,
        Func<T, string> selector)
    {
        return source
            .Select(selector)
            .Subscribe(s => tmpText.text = s);
    }
}