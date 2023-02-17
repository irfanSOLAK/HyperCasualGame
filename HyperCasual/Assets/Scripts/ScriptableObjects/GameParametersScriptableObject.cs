using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameParameters", menuName = "ScriptableObjects/GameParameters", order = 1)]
public class GameParametersScriptableObject : ScriptableObject
{
    #region Camera Parameters

    [TooltipAttribute("Starting position of the Main Camera for GAME 1 and GAME 3.")]
    public Vector3 startingPosition;

    [TooltipAttribute("The parameter that determines how far the Main Camera is behind the character.")]
    public Vector3 cameraToTargetDistance;

    #endregion

    #region Main Menu Parameters

    [TooltipAttribute("Text that appears when the cursor is over the GAME 1 button.")]
    [TextArea] public string game1Info;

    [TooltipAttribute("Text that appears when the cursor is over the GAME 2 button.")]
    [TextArea] public string game2Info;

    [TooltipAttribute("Text that appears when the cursor is over the GAME 3 button.")]
    [TextArea] public string game3Info;

    [TooltipAttribute("Text that appears when the cursor is NOT over the buttons.")]
    [TextArea] public string defaultText;

    #endregion

    #region Game Starter Parameters

    [TooltipAttribute("Countdown in seconds before the game starts. It is also determines GameplayInfo times.")]
    public int countdownInSeconds;

    [TooltipAttribute("Define how to play the game for Game1 and Game3. The text appears the begining of the game.")]
    [TextArea] public string runningGameplayInfo;

    [TooltipAttribute("Define how to play the game for Game2. The text appears the begining of the game.")]
    [TextArea] public string paintingGameplayInfo;

    #endregion

    #region Character Movement Parameters

    [TooltipAttribute("Count down in seconds before the game starts.")]
    public float characterForwardSpeed;

    [TooltipAttribute("Count down in seconds before the game starts.")]
    public float characterHorizontalSpeed;

    [TooltipAttribute("Count down in seconds before the game starts.")]
    public float characterXAxisDisplacement;

    [TooltipAttribute("Count down in seconds before the game starts.")]
    public Vector2 characterXAxisLimits;

    #endregion

    #region Paint Parameters

    [TooltipAttribute("Set cursor to this texture when painting the wall in Game2.")]
    public Texture2D brushPainted;

    [TooltipAttribute("Set cursor to this texture when NOT painting the wall in Game2.")]
    public Texture2D brushUnpainted;

    [TooltipAttribute("Adjust the size of the brush.")]
    public float brushCursorSize;

    [TooltipAttribute("Painting size of the brush in Game2. The bigger values leave the lower dots.")]
    public Vector2Int paintingSize;

    [TooltipAttribute("The color when painting the wall in Game2.")]
    public Color paintingColor;


    #endregion

    #region Ranking Parameters

    [TooltipAttribute("Ranking text color if the player is completed the parkour.")]
    public Color runningTextColor;

    [TooltipAttribute("Ranking text color if the player is NOT completed the parkour.")]
    public Color runningCompletedTextColor;


    #endregion
}
