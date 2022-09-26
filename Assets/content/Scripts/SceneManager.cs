using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public GameObject Platform;
    private Vector3 _DefaultPlatformSize;
    private float _DefaultRotation;
    private float _AngularVelocity;
    private float _CameraZoom;

    private Bkpk.AvatarInfo[] _AvatarsMetadata;
    private Bkpk.BkpkAvatar _Avatar = null;
    private int _CurrentAvatarIndex = 0;

    void Awake()
    {
        Bkpk.Config.ClientID = "b2e14bcc-8728-4c2f-8af1-b539aaaa122e";
        Bkpk.Config.IpfsGateway = "https://ipfs.mona.gallery";

        _DefaultRotation = Platform.transform.rotation.eulerAngles.y;
        _DefaultPlatformSize = Platform.transform.localScale;
        _CameraZoom = Camera.main.fieldOfView;
    }

    void FetchAvatars()
    {
        Bkpk.Auth.Instance.RequestAuthorization(OnAuthorized);
    }

    async void OnAuthorized(string token)
    {
        _AvatarsMetadata = await Bkpk.Avatars.GetAvatars();
        _CurrentAvatarIndex = 0;
        LoadCurrentAvatar();
    }

    async void LoadCurrentAvatar()
    {
        if (_Avatar != null)
        {
            _Avatar.Dispose();
        }
        _Avatar = await Bkpk.Avatars.LoadAvatar(_AvatarsMetadata[_CurrentAvatarIndex]);
        _Avatar.gameObject.transform.SetParent(Platform.transform);
    }

    void Update()
    {
        CycleAvatars();

        // Get mouse input
        if (Input.GetMouseButton(0))
        {
            _AngularVelocity -= Input.GetAxis("Mouse X") * 5;
            Platform.transform.localScale = _DefaultPlatformSize * 0.9f;
        }

        if (_Avatar != null && _Avatar.Animator != null)
        {
            // Get keyboard input
            if (Input.GetKey(KeyCode.W))
            {
                _Avatar.Animator.SetFloat("Speed", 6);
            }
            else
            {
                _Avatar.Animator.SetFloat("Speed", 0);
            }
        }

        // Zoom camera
        Camera.main.fieldOfView = Mathf.Clamp(
            Camera.main.fieldOfView * (1 + Input.GetAxis("Mouse ScrollWheel") * 1f),
            10,
            100
        );

        Platform.transform.localScale = Vector3.Lerp(
            Platform.transform.localScale,
            _DefaultPlatformSize,
            2 * Time.deltaTime
        );
        Platform.transform.rotation = Quaternion.Euler(0, _DefaultRotation + _AngularVelocity, 0);
    }

    void CycleAvatars()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            int index = _CurrentAvatarIndex - 1;
            if (index < 0)
                index = _AvatarsMetadata.Length;
            _CurrentAvatarIndex = index;
            LoadCurrentAvatar();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            int index = _CurrentAvatarIndex + 1;
            if (index == _AvatarsMetadata.Length)
                index = 0;
            _CurrentAvatarIndex = index;
            LoadCurrentAvatar();
        }
    }
}
