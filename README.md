# unity-take-flight
Unity SDK Template showcasing how to do a seamless onboarding experience for users using social logins and the ability to submit your score on-chain. 

## Features
- Embedded Wallets
- Smart Wallets
- Custom Contract Interactions
- Supports WebGL, Standalone

## Scenes

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

## Screenshots

![Login page](https://github.com/thirdweb-example/unity-take-flight/assets/57885104/0dc97972-2d66-4716-8385-81a90d29d00f)

![In-game Screenshot](https://github.com/thirdweb-example/unity-take-flight/assets/57885104/08500a58-7513-42c1-9b53-666908b5feca)

![Game over screen](https://github.com/thirdweb-example/unity-take-flight/assets/57885104/a7c75ba1-6d27-40c2-8440-bd61154c883b)




## Documentation

- To build the game, follow the [build instructions](https://github.com/thirdweb-dev/unity-sdk#build)
- Learn more from the [Unity SDK Documentation](https://portal.thirdweb.com/unity)


## Contributing

Contributions are always welcome! See our [open source page](https://thirdweb.com/open-source) for more information. 


## Support 

For help or feedback, please [visit our support site](https://thirdweb.com/support)



