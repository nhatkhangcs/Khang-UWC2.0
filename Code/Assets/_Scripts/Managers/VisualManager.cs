using UnityEngine;


public class VisualManager : PersistentSingleton<VisualManager>
{
    // @formatter:off
    
    [Header("Common")]
    public float InactiveViewButtonAlpha;
    
    [Header("Theme")]
    public Color PrimaryColor;
    public Color SecondaryColor;
    public Color SubtleGreyColor;
    public Color PrimaryTextColor;
    public Color SecondaryTextColor;
    public Color StaffEntityColor;
    public Color WhiteTextColor;

    [Header("MCPs")] 
    public Color MCPNotFullColor;
    public Color MCPAlmostFullColor;
    public Color MCPFullyLoadedColor;
    
    [Header("Animations")]
    public float ListAndPanelTime;

    // @formatter:on

    public Color GetMCPColor(float percentage)
    {
        if (percentage < SystemConstants.MCP.AlmostFullThreshold) return MCPNotFullColor;
        if (percentage < SystemConstants.MCP.FullyLoadedThreshold) return MCPAlmostFullColor;
        return MCPFullyLoadedColor;
    }

    public string GetMCPStatusText(float percentage)
    {
        if (percentage < SystemConstants.MCP.AlmostFullThreshold)
            return LanguageTranslation.GetText(LanguageTranslation.TextType.MCP_Not_Full,
                LanguageTranslation.ReturnTextOption.Sentence_case);
        if (percentage < SystemConstants.MCP.FullyLoadedThreshold)
            return LanguageTranslation.GetText(LanguageTranslation.TextType.MCP_Almost_Full,
                LanguageTranslation.ReturnTextOption.Sentence_case);
        return LanguageTranslation.GetText(LanguageTranslation.TextType.Setting_MCPs_Fully_Loaded,
            LanguageTranslation.ReturnTextOption.Sentence_case);
    }
}

public enum ColorThemeInheritorType
{
    PrimaryColor,
    SecondaryColor,
    SubtleGreyColor,
    StaffEntityColor,
}

public enum TextThemeInheritorType
{
    PrimaryTextColor,
    SecondaryTextColor,
    WhiteTextColor,
}