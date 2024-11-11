# Unity WebGL Microphone

Microphone interface for Unity when using WebGL build target. Matches standard Unity builtin Microphone class in interface so you can it for multiplatform projects without preprocessor defines. Pulls data from Web Audio into Unity AudioClips so it behaves roughly the same as the Microphone on other platforms.

## Installation

Add a git package in the Unity package manager (UPM).

```
https://github.com/bnco-dev/unity-webgl-microphone.git
```

See UPM instructions [here](https://docs.unity3d.com/Manual/upm-ui-giturl.html).

## Usage

This is intended to be a drop-in replacement for Unity's [Microphone](https://docs.unity3d.com/ScriptReference/Microphone.html) class. Microphone code you have working on other platforms should work on WebGL too with minimal changes.

Here's a (hopefully) exhaustive list of the differences:

1) If `Start()` is called before the user has interacted with the web page, it can fail and return null. This is a limitation of the web. Specifically, AudioContexts cannot be started until the first user interaction. One solution is to only call `Start()` after a user must have interacted with the page (i.e., after something has been clicked). You can also safely just keep calling it every few seconds until it returns a clip.

2) Calling `Start()`, `GetPosition()`, `IsRecording()` or `GetDeviceCaps()` will kick off the permissions procedure in the user's browser. During this time `Start()` will provide clips, but they will not be filled with data. You can use `IsRecording()` to check whether data is currently coming in. This is similar to other platforms as there is usually a delay between Audio Clip creation and microphone input. Here though the delay time may take longer, and data may never come in if the user does not give microphone permission. There is currently no way to test if the user has declined permission. 

3) This package can only access the default recording device. It's probably possible to target a specific device, but it's not implemented here. Best practice for the web seems to be to use the default device anyway.

4) The recording is always looped and frequency is left to the browser to decide. Though it's possible in theory to specify your desired frequency in the browser, this only led to audio issues when I tested it.

5) `Start()` will set the generated audio clips to the specified length, but they may be up to 512 samples shorter or longer than expected to simplify ring buffer behaviour in the plugin.

6) Unity's audio API has [limitations](https://docs.unity3d.com/Manual/webgl-audio.html) on WebGL.

## Compatibility

| Unity       | Browser      | Read-In            | Loopback           |
|-------------|--------------|--------------------|--------------------|
|             |              |                    |                    |
|             | Firefox 107  | :white_check_mark: | :x:                |
| 2021.3.29f1 | Edge 108     | :white_check_mark: | :white_check_mark: |
|             | Chromium 108 | :white_check_mark: | :white_check_mark: |
|             |              |                    |                    |
|             | Firefox 107  | :white_check_mark: | :x:                |
| 2022.3.28f1 | Edge 108     | :white_check_mark: | :white_check_mark: |
|             | Chromium 108 | :white_check_mark: | :white_check_mark: |
|             |              |                    |                    |
|             | Firefox 107  | :white_check_mark: | :x:                |
| 6000.0.2f1  | Edge 108     | :white_check_mark: | :white_check_mark: |
|             | Chromium 108 | :white_check_mark: | :white_check_mark: |


Pull requests welcome. Enjoy :)