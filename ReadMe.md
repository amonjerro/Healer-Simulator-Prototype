## Healer Simulator Prototype

2D prototype for the healer simulator game. Players control a healer who has AI companions that fight for them. Players must keep themselves and their companions healed or risk being overwhelmed by enemy slimes.

## System Architecture

Documentation for the architecture of the systems is shown below

### Character 

Characters are composed using a combination of the [Observer pattern](https://refactoring.guru/design-patterns/observer) and [MVC pattern](https://en.wikipedia.org/wiki/Model%E2%80%93view%E2%80%93controller) to facilitate encapsulation and keep coupling low. Below is a diagram of class relationships and dependencies.

Message transmission in this arrangement is important to understand. When one of the components has an event occur it communicates it to the EventManager which publishes it to all suscribers to the corresponding channel. There are currently three channels:
- onCharacterEvent: The general channel for all events concerning this character (say, the controller sends out an order to move in a particular direction).
- onStatusChange: Channel for communicating important status changes for the character (say, the character dies)
- onUIRequest: Channel for communicating to the UI and keep the component classes unbothered.

Below is a simplified sequence diagram of how that communication works. This separation keeps the components decoupled as they don't need to know about each other's existence or specific type.

### Abilities

In an effort to be as data-driven as possible, the abilities class hierarchy strongly separates data from behavior. Most abilities share very common behavior (e.g. affect a single target, apply a status effect, so on) and then resolve that behavior by making changes to the underlying data of a specific character based on their own data specifications. As such, ability contents are declared as ScriptableObjects and which are then wrapped around the general ability behavior. This allows for designers to quickly create and add new skils without having to extend the code. Below is a diagram of the class architecture.


To see how abilities are used, the following sequence diagram illustrates the message chain that governs the communication of events throughout the ability lifecycle.

### AI

The AI works by inheriting from the `CharacterController` as it takes control of how an NPC will move. It has a `StateMachine` that governs its overall decision making. However, some specific tasks mandated by the `StateMachine` will vary in relation to the type of character the AI is playing. For instance, the way the TankAlly is meant to choose targets to attack might differ from the way an EnemySlime would. To handle this, a [Strategy pattern](https://refactoring.guru/design-patterns/strategy) approach is employed, delegating the evaluation and execution details to the specific strategies. 

### Services

There are a few singletons handling overarching game states. These services should not know about each other, since their specific domains are extremely separate but events under each one might be relevant to the others. Thus, to reduce the amount of static singletons floating around and to ensure decoupling, a single static `ServiceLocator` exists that any class can address to get reference a specific service. Services themselves publish events through the `ServiceLocator` which in turn broadcasts those events to its subscribed services. This way, adding or removing services should not affect the functioning of any specific service, since they don't have any direct dependencies or references.

Below is a diagram showing that architecture.

### UI

The UI classes in this prototype have been kept extremely simple, given that the purpose of the prototype was not to develop a user interface but a working game. That said, every party member has their stats displayed on screen for easy reference given that knowledge of the party's health is crucial for a game about healing. In addition to that, the `PlayerCharacterController` also emits messages to the `Powers` UI, so the player knows what spells are available at what time.

### Utils

Some interesting utility classes also exist in this prototype, chief amongst them the `TimeUtil` which acts as a wrapper around the Unity clock to create the possibility of changing the time scale for some elements of the game but not the entire game.