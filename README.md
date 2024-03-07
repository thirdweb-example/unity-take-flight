# take-flight
 Unity SDK Starter Template - Embedded Wallets, Smart Wallets and Custom Contract Interactions.
 
![takeflight](https://github.com/thirdweb-example/take-flight/assets/43042585/bd207d4b-9223-479d-a664-31cb96be03f1)

Contains a single scene:
- `00_Scene_Main`: Contains all game and blockchain elements, including onboarding flow. Blockchain elements are mostly managed in the `BlockchainManager`.

Platforms supported: WebGL, Standalone.

Test in WebGL here: https://thirdweb-example.github.io/unity-take-flight/

 ## Setup Instructions
 1. Clone this repository.
 2. Open in Unity 2022.3.17f1
 3. Create a [thirdweb api key](https://thirdweb.com/create-api-key)
 4. Make sure `com.thirdweb.takeflight` is an allowlisted bundle id for your API key, and enable Smart Wallets.
 5. If testing in WebGL, set allowlisted domains to `*` or to your localhost url. (Note: known issue - you may need to host your WebGL build on something like github pages for Embedded Wallets to work properly temporarily)
 6. Find your `ThirdwebManager` in `00_Scene_Main` and set the client id there.
 7. Press Play!

To build the game, make sure you follow our build instructions [here](https://github.com/thirdweb-dev/unity-sdk#build).
