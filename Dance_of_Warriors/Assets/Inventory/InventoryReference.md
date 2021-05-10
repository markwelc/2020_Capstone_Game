<h3>Inventory Reference</h3>
<ul>
    <li>Inventory.cs</li>
        <ul>
            <li>public void AddItem(IInventoryItem item)</li>
                <ul>
                    <li>param item: references the object of the item that is being added to inventory. </li>
                </ul>
            <li>public void UseItem(IInventoryItem item)</li>
                <ul>
                    <li>param item: references the object of the item that is being used in the inventory.</li>
                </ul>
            <li>private void Awake()</li>
                <ul>
                    <li>Initializes player controls and maps the inventory button</li>
                </ul>
            <li>void OnEnable()</li>
                <ul>
                    <li>Enables the player controls</li>
                </ul>
            <li>void OnDisable()</li>
                <ul>
                    <li>Disables the player controls</li>
                </ul>
            <li>void inventoryButton()</li>
                <ul>
                    <li>Handles displaying the inventory and disabling camera movement while inventory is active.</li>
                </ul>
        </ul>
    <p><p>
    <li>InventoryItem.cs</li>
        Provides interface to implement inventory items
        <ul>
            <ul>
                <li>String Name: The name of the object</li>
                <li>Sprite Image: The sprite/image of the object, displayed in inventory and set in Unity</li>
                <li>void OnPickup(): Defines the object's behavior on character pick up. Generally just disables the object in the environment</li>
                <li>void OnUse(): Defines the object's behavior when selected from the inventory</li>
            </ul>
            <li>DocBag.cs</li>
                <ul>
                    <li>Healing Item</li>
                    <li>Implements InventoryItem interface</li>
                    <li>Heals player's limbs</li>
                </ul>
            <li>HealthPack.cs</li>
                <ul>
                    <li>Healing Item</li>
                    <li>Implements InventoryItem interface</li>
                    <li>Heals player's health</li>
                </ul>
            <li>Cube.cs</li>
                <ul>
                    <li>Testing item</li>
                    <li>Implements InventoryItem interface</li>
                    <li>Used to test implementation of inventory system</li>
                    <li>Unused</li>
                </ul>
        </ul>
    <p><p>
    <li>ItemClickHandler.cs</li>
        <ul>
            <li>Handles item references when selected wtih the cursor</li>
            <ul>
                <li>Public Void OnItemClicked(): Gets reference to item being selected in inventory, and calls item's OnUse() method.</li>
            </ul>
        </ul>
    <p><p>
    <li>ItemDragHandler.cs</li>
        <ul>
            <li>Handles dragging items to and from inventory with the cursor</li>
        </ul>
    <p><p>
    <li>HUD.cs</li>
        <ul>
            <li>void Start()</li>
                <ul>
                    <li>Initializes inventory objects and event callers</li>
                </ul>
            <li>private void InventoryScript_ItemAdded(object sender, InventoryEventArgs e)</li>
                <ul>
                    <li>param sender: Not used in function</li>
                    <li>param e: Reference to object calling this function - used for setting image in inventory slot</li>
                </ul>
            <li>public void InventoryScript_ButtonPressed(object sender, InventoryEventArgs e)</li>
                <ul>
                    <li>param sender: Not used in function</li>
                    <li>param e: Reference to object calling this function - used for setting image in inventory slot</li>
                </ul>
            <li>public void OpenPickupMessagePanel(string text)</li>
                <ul>
                    <li>Handles opening pickup message when player is near an inventory item</li>
                    <li>param text: Message to be displayed in pickup message panel</li>
                </ul>
            <li>public void ClosePickupMessagePanel()</li>
                <ul>
                    <li>Handles closing pickup message when player is near an inventory item and picks it up</li>
                </ul>
            <li>public void OpenDeathMessagePanel()</li>
                <ul>
                    <li>Opens losing screen when player dies</li>
                </ul>
            <li>public void OpenWinMessagePanel()</li>
                <ul>
                    <li>Opens winning screen when player kills the enemy</li>
                </ul>
        </ul>
</ul>