# Metaverse Backpack Dressing Room Sample

Example WebGL Demo of the Metaverse Backpack

## Getting started

### Setup

`$ git clone github.com/metaverse-backpack/unity-dressing-room-sample`

`$ cd unity-dressing-room-sample`

`$ git submodule update --init --recursive`

### Build settings

Open up the Unity project in the Unity Hub. If there is a popup asking if you want to open the project in safe mode, click on `Ignore`. This is because the project starts with a native build target causing dependencies not to be loaded. Change the Build target to WebGL by choosing `File -> Build Settings...`. Then select `WebGL` in the `Platform` selection panel. Ensure `Scene` is selected under `Scenes in Build`.

### Build and run

Select `File -> Build And Run` to start a unity development web server and run the project in a browser (`http://localhost:51326`).

### Additional settings

The dressing room is authorized via OAuth2 to read from Backpack. You can use the client ID `f596bf0f-1fb9-4826-95e9-3c1e8a90a137` for development purposes. The client ID can be set in `Assets/Scripts/SceneManager.cs`. It's important to use a development client ID as the redirect URI is validated by the server and rejected if not matching with the production deployment webserver.

## Engine version

[Unity 2021.3.10f1](https://unity3d.com/unity/whats-new/2021.3.10)
