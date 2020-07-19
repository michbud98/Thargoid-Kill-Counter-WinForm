[![AXI Discord](https://img.shields.io/discord/164411426939600896.svg?style=social&label=AXI%20Discord)](https://discord.gg/gZbAWCF)


# Thargoid-Kill-Counter
## Features
 - Calculates thargoid kills while playing Elite Dangerous
 - Saves screenshot of Thargoid Interceptor variant when player kills it(must be enabled in settings)

## Usage
### Download and first run
1. Download [zip file](https://github.com/Prorok9999/Thargoid-Kill-Counter/releases/latest) and extract it's contents inside folder of your choice. **Keep application and config inside the folder or application won't work.**
2. Run TKC exe file. At first run app is going to try to locate default Elite Dangerous log files directory.
- If it is succesful the app will print out current kills. 
- If app is not able to find default path to logs directory it will ask you to select it with folder browser. Once application finds logs directory it  will remember its location on next start. **From that moment you can just start the application and it will calculate kills immediately.**

### Changing logs directory
If you want application to read log files from different directory:
1. Select user -> settings. This will open a new window with basic settings in which you find textbox showing path to log files directory.
2. Click on browse button and select directory from folder browser.
3. Check if path to directory changed in text box, after that hit save button.
4. Restart application.

### Enabling/Disabling screenshots of Thargoid kills
If you want application to create screenshots of thargoid kills:
1. Select user -> settings. This will open a new window with basic settings. 
2. Check/Uncheck screenshot thargoid kill check box.
3. Hit save button.
4. Restart application.
**Note: Screenshots are saved inside screenshots folder next to exe file.**

## Acknowledgements
- “Elite: Dangerous” is © 1984 - 2019 Frontier Developments plc.
- Thanks to Anti Xeno Initiative for logo and helpfull tips on thargoid hunting.
- Using [Newtonsoft.Json](https://github.com/JamesNK/Newtonsoft.Json) created by [James Newton-King](https://github.com/JamesNK)
  - Copyright (c) 2007 James Newton-King - [Licence](https://github.com/JamesNK/Newtonsoft.Json/blob/master/LICENSE.md)
- Using [log4net](https://logging.apache.org/log4net/) created by [The Apache Software Foundation](https://www.apache.org)
  - [Licence](https://logging.apache.org/log4net/license.html)

## Licence
- Copyright © 2019 Prorok9999
- Licensed under the [MIT Licence](https://github.com/Prorok9999/Thargoid-Kill-Counter/blob/master/LICENSE)

