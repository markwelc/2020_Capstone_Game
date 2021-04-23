<h3>Sound Reference</h3>
<ul>
    <li>AudioManager.cs</li>
        <ul>
            <li>void Awake()</li>
                <ul>
                    <li>unused</li>
                </ul>
            <li>public void Play (GameObject gameObject, String name)</li>
                <ul>
                    <li>Finds the appropriate sound file to play within a scene</li>
                    <li>param gameObject: References an object to place audio source onto. Used for playing audio from a specific place.</li>
                    <li>param name: String to hold the name of the sound file we want to play.</li>
                </ul>
        </ul>
    <li>LightOnAnyBeat.cs</li>
        <ul>
            <li>void Start()</li>
                <ul>
                    <li>Initializes references to all light sources in scene.</li>
                </ul>
            <li>void Update()</li>
                <ul>
                    <li>Tracks bpm of song and changes lights based on beat.</li>
                </ul>
        </ul>
    <li>LightOnBeat.cs</li>
        <ul>
            <li>Unused</li>
        </ul>
    <li>musicAnalyzer.cs</li>
        <ul>
            <li>
            <li>private void Awake()</li>
                <ul>
                    <li>Initializes music analyzer instance and makes sure it is the only one.</li>
                </ul>
            <li>void Start()</li>
                <ul>
                    <li>Sets up beat/song timing for game music.</li>
                </ul>
            <li>void Update()</li>
                <ul>
                    <li>Calls beatCounter() every frame</li>
                </ul>
            <li>void beatCounter()</li>
                <ul>
                    <li>Maintains count of beats within song played in game environment.</li>
                </ul>
        </ul>
    <li>Sound.cs</li>
        <ul>
            <li>Initializes a sound object for use within the game.</li>
        </ul>
</ul>