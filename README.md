# Unity WebGL Microphone

Microphone interface for Unity when using WebGL/WebXR build target. Matches standard Unity builtin Microphone class in interface so you can it for multiplatform projects without preprocessor defines. Pulls data from Web Audio into Unity AudioClips so it behaves roughly the same as the Microphone on other platforms.

## Installation

Add a git package in the Unity package manager (UPM).

```
https://github.com/bnco-dev/unity-webgl-microphone.git
```

See UPM instructions [here](https://docs.unity3d.com/Manual/upm-ui-giturl.html).

## Known Issues

1) If Start() is called before the user has interacted with the web page,
it can fail and return null. Typical solution is to only call Start()
after a user must have interacted with the page (i.e., after something
has been clicked). You can also safely just keep calling Start() every
few seconds until it returns a clip.

2) Ignores deviceName arguments and only lists the default device. It's
probably possible to target a specific device, just not implemented here.
Best practice for the web seems to be to use the default device anyway.

3) All arguments to Start() are ignored. The clip length is fixed to
a safe default, is always looped, and frequency is left to the browser to
decide. Though it's possible in theory to specify your desired frequency
in the browser, this only led to audio issues when I tested it.

4) Unity's audio API has [limitations](https://docs.unity3d.com/Manual/webgl-audio.html) on WebGL.

## Compatibility

Edge 108, read-in :white_check_mark:, loopback :white_check_mark:
Chromium 108, read-in :white_check_mark:, loopback :white_check_mark:
Firefox 107, read-in :white_check_mark:, loopback :x:

Pull requests welcome. Enjoy :)