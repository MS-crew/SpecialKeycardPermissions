<h1 align="center">Special Keycard Permission</h1>
<div align="center">
<a href="https://github.com/MS-crew/SpecialKeycardPermissions"><img src="https://img.shields.io/github/actions/workflow/status/Exiled-Team/EXILED/main.yml?style=for-the-badge&logo=githubactions&label=build" href="https://github.com/MS-crew/SpecialKeycardPermissions" alt="GitHub Source Code"></a>
<a href="https://github.com/MS-crew/SpecialKeycardPermissions/releases"><img src="https://img.shields.io/github/downloads/MS-crew/SpecialKeycardPermissions/total?style=for-the-badge&logo=githubactions&label=Downloads" href="https://github.com/MS-crew/SpecialKeycardPermissions/releases" alt="GitHub Release Download"></a>
<a href="https://github.com/MS-crew/SpecialKeycardPermissions/releases"><img src="https://img.shields.io/badge/Build-1.0.1-brightgreen?style=for-the-badge&logo=gitbook" href="https://github.com/MS-crew/SpecialKeycardPermissions/releases" alt="GitHub Releases"></a>
<a href="https://github.com/MS-crew/SpecialKeycardPermissions/blob/master/LICENSE"><img src="https://img.shields.io/badge/Licence-GNU_3.0-blue?style=for-the-badge&logo=gitbook" href="https://github.com/MS-crew/SpecialKeycardPermissions/blob/master/LICENSE" alt="General Public License v3.0"></a>
<a href="https://github.com/ExMod-Team/EXILED"><img src="https://img.shields.io/badge/Exiled-8.13.1-red?style=for-the-badge&logo=gitbook" href="https://github.com/ExMod-Team/EXILED" alt="GitHub Exiled"></a>

</div>

## Special Keycard Permission

- **Special doors:** Allowing some doors to be opened with special cards (for example, the Wc can be opened with the janitor's card or gate A can only be opened with the chaos card).
- **Special card permissions:** You can install custom card permissions on a card(for example, like the facility guard card can open the micro).

## Installation

1. Download the release file from the GitHub page [here](https://github.com/MS-crew/SpecialKeycardPermissions/releases).
2. Extract the contents into your `\AppData\Roaming\EXILED\Plugins` directory.
3. Configure the plugin according to your server’s needs using the provided settings.
4. Restart your server to apply the changes.

## Feedback and Issues

This is the initial release of the plugin. We welcome any feedback, bug reports, or suggestions for improvements.

- **Report Issues:** [Issues Page](https://github.com/MS-crew/SpecialKeycardPermissions/issues)
- **Contact:** [discerrahidenetim@gmail.com](mailto:discerrahidenetim@gmail.com)

Thank you for using our plugin and helping us improve it!
## Default Config
```yml
Is_enabled: true
debug: false
# This list shows which card will change your permissions.
special_permission:
  KeycardJanitor:
  - ContainmentLevelOne
  - ArmoryLevelTwo
  - Intercom
# In this list you can add a special door that can only be opened with the cards you specify.
special_door_list:
  LczWc:
  - KeycardJanitor
  - KeycardGuard
  GateA:
  - KeycardChaosInsurgency
```
