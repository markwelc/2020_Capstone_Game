<h3>Environment Reference</h3>
<ul>
    <li>loadLevel.cs</li>
        <ul>
            <li>loads a scene from the game files</li>
            <li>public void loadScene(int sceneIdx)<li>
                <ul>
                    <li>Calls corouting to asynchronously load game files for scene.</li>
                    <li>param idx: index of scene to be loaded from game files.</li>
                </ul>
            <li>IEnumerator asyncLoad(int sceneIdx)</li>
                <ul>
                    <li>Shows loading screen and loads all scene files before allowing player to enter.</li>
                    <li>param sceneIdx: index of scene to load from game files.</li>
                </ul>
        </ul>
    <p><p>
    <li>doorController.cs</li>
        <ul>
            <li>void Start()</li>
                <ul>
                    <li></li>
                </ul>
            <li>private void OnTriggerEnter(Collider other)</li>
                <ul>
                    <li>Opens door when player enters sphere collider.</li>
                    <li>param other: reference to the player character's collider.</li>
                </ul>
            <li>private void OnTriggerExit(Collider other)</li>
                <ul>
                    <li>Closes door when player leaves sphere collider.</li>
                    <li>param other: reference to the player character's collider.</li>
                </ul>
        </ul>
</ul>