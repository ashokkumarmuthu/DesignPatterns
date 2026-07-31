# Command Pattern Challenge: Smart Home Automation Remote & Macro Pipeline 🏠

### Scenario
You are designing the software for a high-end Smart Home Automation Controller. The controller manages multiple hardware devices in a home:
- **Smart Light:** Turn ON/OFF, Set Brightness (0-100%).
- **Thermostat:** Set Target Temperature, Turn HVAC ON/OFF.
- **Smart Security Lock:** Lock/Unlock Door.

Users want to execute individual actions OR trigger composite **Macro Routines** (e.g., "Good Night Routine": turns off all lights, sets thermostat to 68°F, and locks all doors with a single button press). 

Furthermore, if a user accidentally activates a macro or setting, they can hit **UNDO** to revert the entire routine or single action back to its previous state.

---

### What it teaches:
* **The Command Design Pattern:** Encapsulating device actions into self-contained `ICommand` objects with `Execute()` and `Undo()`.
* **The Composite Command (Macro Pattern):** Composing multiple commands into a single macro command object.
* **Invoker & History Stack:** Managing undo stacks and execution logs without coupling the remote to specific smart hardware receivers.

---

### Core Requirements
1. **Receiver Hardware Classes:**
   - `SmartLight`: `IsOn`, `BrightnessLevel`.
   - `SmartThermostat`: `TargetTemp`, `IsHvacActive`.
   - `SmartLock`: `IsLocked`.
2. **Command Interface (`ICommand`):**
   - `Execute()`, `Undo()`, `Description` (property).
3. **Concrete Commands:**
   - `ToggleLightCommand`
   - `SetThermostatCommand`
   - `LockDoorCommand`
   - `MacroCommand`: Holds `List<ICommand>`, executes sequentially, undos in reverse order!
4. **Invoker (`SmartHomeRemoteController`):**
   - Stores undo stack `Stack<ICommand>` and command audit log.
   - Provides `PressButton(ICommand)` and `PressUndoButton()`.
