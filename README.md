## About

This project is from the BabylonJS Guided Learning Create a Game Tutorial Series, the different is that this project is built on top of Blazor Wasm using EventHorizon.Blazor.TypeScript.Interop.Generator to generate a C# proxy layer to the talk with the BabylonJS Framework. 

This show cases Blazor Wasm being used with a relativity complicated JavaScript Framework to create a functional Game.

## EventHorizon.Blazor.TypeScript.Interop.Generator

This is a project I, Cody Merritt Anhorn, personally created to help with the generation of the .NET/C# proxy layer. This proxy layer creates an interop layer to the underlying JavaScript having an almost exact API that would be used if the game was built in JavaScript/TypeScript.

[Original Game](https://babylonjs.github.io/SummerFestival/)

## Commands 

~~~ bash
# Setup .NET Http SSL
dotnet dev-certs https --trust

# Install EHZ Generator Tool
dotnet tool install -g EventHorizon.Blazor.TypeScript.Interop.Tool

# Update EHZ Generator Tool
dotnet tool update -g EventHorizon.Blazor.TypeScript.Interop.Tool

# Generated BabylonJS project interop with multiple sources.
ehz-generate -c Scene -c Engine -c DebugLayer -c HemisphericLight -c PointLight -c ArcRotateCamera -c UniversalCamera -c PBRMetallicRoughnessMaterial -c MeshBuilder -c SceneLoader -c StandardMaterial -c ExecuteCodeAction -c AdvancedDynamicTexture -c Button -c StackPanel -c Sound -c CubeTexture -c GlowLayer -a Blazor.BabylonJS.WASM -s https://cdn.jsdelivr.net/gh/BabylonJS/Babylon.js@4.2.0/dist/babylon.d.ts -s https://cdn.jsdelivr.net/gh/BabylonJS/Babylon.js@4.2.0/dist/gui/babylon.gui.d.ts
~~~

# Licensing
## Art Assets
- [models](https://github.com/capucat/summers-festival/tree/master/models)
- [sprites](https://github.com/capucat/summers-festival/tree/master/sprites)
- [textures](https://github.com/capucat/summers-festival/tree/master/textures)

<a rel="license" href="http://creativecommons.org/licenses/by/4.0/"><img alt="Creative Commons License" style="border-width:0" src="https://i.creativecommons.org/l/by/4.0/88x31.png" /></a><br />This work is licensed under a <a rel="license" href="http://creativecommons.org/licenses/by/4.0/">Creative Commons Attribution 4.0 International License</a>.  
Art by Bianca Guerrero (capucat)

## Music
["copycat"](https://opengameart.org/content/copycat) by syncopika is licensed under [CC-BY 3.0](https://creativecommons.org/licenses/by/3.0/)  
["Snowland Town"](https://opengameart.org/content/snowland-town) by Matthew Pablo is licensed under [CC-BY 3.0](https://creativecommons.org/licenses/by/3.0/)  
http://www.matthewpablo.com  
["Jump2"](https://freesound.org/people/LloydEvans09/sounds/187024/) by LloydEvans09 is licensed under [CC-BY 3.0](https://creativecommons.org/licenses/by/3.0/)

## Other Assets
Unless specified, all other assets are licensed under [CC0](https://creativecommons.org/publicdomain/zero/1.0/)
