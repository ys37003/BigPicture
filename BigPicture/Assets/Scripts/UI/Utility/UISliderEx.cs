//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2016 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;
using System;

/// <summary>
/// Extended progress bar that has backwards compatibility logic and adds interaction support.
/// </summary>

[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/NGUI SliderEx")]
public class UISliderEx : UIProgressBar
{
    enum Direction
    {
        Horizontal,
        Vertical,
        Upgraded,
    }

    // Deprecated functionality. Use 'foregroundWidget' instead.
    [HideInInspector] [SerializeField] Transform foreground = null;

    // Deprecated functionality
    [HideInInspector] [SerializeField] float rawValue = 1f; // Use 'value'
    [HideInInspector] [SerializeField] Direction direction = Direction.Upgraded; // Use 'fillDirection'
    [HideInInspector] [SerializeField] protected bool mInverted = false;

    [HideInInspector] [SerializeField] float breakMin = 0, breakMax = 1;

    /// <summary>
    /// Whether the collider is enabled and the widget can be interacted with.
    /// </summary>

    public bool isColliderEnabled
    {
        get
        {
            Collider c = GetComponent<Collider>();
            if (c != null) return c.enabled;
            Collider2D b = GetComponent<Collider2D>();
            return (b != null && b.enabled);
        }
    }

    [System.Obsolete("Use 'value' instead")]
    public float sliderValue { get { return this.value; } set { this.value = value; } }

    [System.Obsolete("Use 'fillDirection' instead")]
    public bool inverted { get { return isInverted; } set { } }

    /// <summary>
    /// Upgrade from legacy functionality.
    /// </summary>

    protected override void Upgrade()
    {
        if (direction != Direction.Upgraded)
        {
            mValue = rawValue;

            if (foreground != null)
                mFG = foreground.GetComponent<UIWidget>();

            if (direction == Direction.Horizontal)
            {
                mFill = mInverted ? FillDirection.RightToLeft : FillDirection.LeftToRight;
            }
            else
            {
                mFill = mInverted ? FillDirection.TopToBottom : FillDirection.BottomToTop;
            }
            direction = Direction.Upgraded;
#if UNITY_EDITOR
            NGUITools.SetDirty(this);
#endif
        }
    }

    /// <summary>
    /// Register an event listener.
    /// </summary>

    protected override void OnStart()
    {
#if UNITY_4_3 || UNITY_4_5 || UNITY_4_6 || UNITY_4_7
				GameObject bg = (mBG != null && (mBG.collider != null || mBG.GetComponent<Collider2D>() != null)) ? mBG.gameObject : gameObject;
		UIEventListener bgl = UIEventListener.Get(bg);
		bgl.onPress += OnPressBackground;
		bgl.onDrag += OnDragBackground;

		if (thumb != null && (thumb.collider != null || thumb.GetComponent<Collider2D>() != null) && (mFG == null || thumb != mFG.cachedTransform))
#else
        GameObject bg = (mBG != null && (mBG.GetComponent<Collider>() != null || mBG.GetComponent<Collider2D>() != null)) ? mBG.gameObject : gameObject;
        UIEventListener bgl = UIEventListener.Get(bg);
        bgl.onPress += OnPressBackground;
        bgl.onDrag += OnDragBackground;

        if (thumb != null && (thumb.GetComponent<Collider>() != null || thumb.GetComponent<Collider2D>() != null) && (mFG == null || thumb != mFG.cachedTransform))
#endif
        {
            UIEventListener fgl = UIEventListener.Get(thumb.gameObject);
            fgl.onPress += OnPressForeground;
            fgl.onDrag += OnDragForeground;
        }
    }

    /// <summary>
    /// Update the value of the scroll bar.
    /// </summary>

    public override void ForceUpdate()
    {
        mIsDirty = false;
        bool turnOff = false;

        if (mFG != null)
        {
            UIBasicSprite sprite = mFG as UIBasicSprite;

            if (mValue < breakMin)
                mValue = breakMin;
            if (mValue > breakMax)
                mValue = breakMax;

            if (isHorizontal)
            {
                if (sprite != null && sprite.type == UIBasicSprite.Type.Filled)
                {
                    if (sprite.fillDirection == UIBasicSprite.FillDirection.Horizontal ||
                        sprite.fillDirection == UIBasicSprite.FillDirection.Vertical)
                    {
                        sprite.fillDirection = UIBasicSprite.FillDirection.Horizontal;
                        sprite.invert = isInverted;
                    }

                    sprite.fillAmount = value;
                }
                else
                {
                    mFG.drawRegion = isInverted
                                    ? new Vector4(1f - value, 0f, 1f, 1f)
                                    : new Vector4(0f, 0f, value, 1f);
                    mFG.enabled = true;
                    turnOff = value < 0.001f;
                }
            }
            else if (sprite != null && sprite.type == UIBasicSprite.Type.Filled)
            {
                if (sprite.fillDirection == UIBasicSprite.FillDirection.Horizontal ||
                    sprite.fillDirection == UIBasicSprite.FillDirection.Vertical)
                {
                    sprite.fillDirection = UIBasicSprite.FillDirection.Vertical;
                    sprite.invert = isInverted;
                }

                sprite.fillAmount = value;
            }
            else
            {
                mFG.drawRegion = isInverted
                                ? new Vector4(0f, 1f - value, 1f, 1f)
                                : new Vector4(0f, 0f, 1f, value);
                mFG.enabled = true;
                turnOff = value < 0.001f;
            }
        }

        if (thumb != null && (mFG != null || mBG != null))
        {
            Vector3[] corners = (mFG != null) ? mFG.localCorners : mBG.localCorners;

            Vector4 br = (mFG != null) ? mFG.border : mBG.border;
            corners[0].x += br.x;
            corners[1].x += br.x;
            corners[2].x -= br.z;
            corners[3].x -= br.z;

            corners[0].y += br.y;
            corners[1].y -= br.w;
            corners[2].y -= br.w;
            corners[3].y += br.y;

            Transform t = (mFG != null) ? mFG.cachedTransform : mBG.cachedTransform;
            for (int i = 0; i < 4; ++i) corners[i] = t.TransformPoint(corners[i]);

            if (isHorizontal)
            {
                Vector3 v0 = Vector3.Lerp(corners[0], corners[1], 0.5f);
                Vector3 v1 = Vector3.Lerp(corners[2], corners[3], 0.5f);
                SetThumbPosition(Vector3.Lerp(v0, v1, isInverted ? 1f - value : value));
            }
            else
            {
                Vector3 v0 = Vector3.Lerp(corners[0], corners[3], 0.5f);
                Vector3 v1 = Vector3.Lerp(corners[1], corners[2], 0.5f);
                SetThumbPosition(Vector3.Lerp(v0, v1, isInverted ? 1f - value : value));
            }
        }

        if (turnOff) mFG.enabled = false;
    }
    
    /// <summary>
         /// Position the scroll bar to be under the current touch.
         /// </summary>

    protected void OnPressBackground(GameObject go, bool isPressed)
    {
        if (UICamera.currentScheme == UICamera.ControlScheme.Controller) return;
        mCam = UICamera.currentCamera;
        value = ScreenToValue(UICamera.lastEventPosition);
        if (!isPressed && onDragFinished != null) onDragFinished();
    }

    /// <summary>
    /// Position the scroll bar to be under the current touch.
    /// </summary>

    protected void OnDragBackground(GameObject go, Vector2 delta)
    {
        if (UICamera.currentScheme == UICamera.ControlScheme.Controller) return;
        mCam = UICamera.currentCamera;
        value = ScreenToValue(UICamera.lastEventPosition);
    }

    /// <summary>
    /// Save the position of the foreground on press.
    /// </summary>

    protected void OnPressForeground(GameObject go, bool isPressed)
    {
        if (UICamera.currentScheme == UICamera.ControlScheme.Controller) return;
        mCam = UICamera.currentCamera;

        if (isPressed)
        {
            mOffset = (mFG == null) ? 0f :
                value - ScreenToValue(UICamera.lastEventPosition);
        }
        else if (onDragFinished != null) onDragFinished();
    }

    /// <summary>
    /// Drag the scroll bar in the specified direction.
    /// </summary>

    protected void OnDragForeground(GameObject go, Vector2 delta)
    {
        if (UICamera.currentScheme == UICamera.ControlScheme.Controller) return;
        mCam = UICamera.currentCamera;
        value = mOffset + ScreenToValue(UICamera.lastEventPosition);
    }

    /// <summary>
    /// Watch for key events and adjust the value accordingly.
    /// </summary>

    public override void OnPan(Vector2 delta) { if (enabled && isColliderEnabled) base.OnPan(delta); }

    [ExecuteInEditMode]
    public void InitSliderEx()
    {
        mFG   = transform.Find("Forground").GetComponent<UIWidget>();
        mBG   = transform.Find("Background").GetComponent<UIWidget>();
        thumb = transform.Find("Thumb");
    }
}