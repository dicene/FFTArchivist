# FFTArchivist

#### Work in Progress Mod Creation tool for Final Fantasy Tactics - The Ivalice Chronicles utilizing [FF16Tools](https://github.com/Nenkai/FF16Tools) for modifying the games data files.

## Current Functionality:
- Read basic properties related to Items, Abilities, and Poaching.
- Reads data from mixed sources, including NEX databases stored in the game's .pac files, as well as pulling data directly from the executable itself, without the need to extract or convert those files ahead of time.
- The fields contained within a single view can come from any number of different sources, reducing the need to jump back and forth between several tables, and avoiding displaying fields that are not actually used by the application (such as the JPCost field in the executable's built-in Ability table, which is always overridden by the Abilities NEX table.
- Utilizes an MVVM pattern that should make it easy to add more fields and tables in the future. WPF bindings should help to minimize bugs related to losing track of data state.
- Through additional logic in the editor ViewModels, controls such as dropdown lists can be populated with the names of the items you are choosing through the list, rather than just displaying the ID or Row Number of the related entries in other tables. (Example: Displaying the name of items in the Poaching editor, instead of just the ID of items).

## Planned Features:
- Saving modified fields to their appropriate data sources.
- Reading data directly from existing FFTIVC mods for inspecting the mods that others have created.
- Packaging all relevant and modified files directly into archives compatible with Reloaded II for easy sharing and installation.
- Custom controls for displaying data that might be difficult to visualize/edit in number/text form such as selecting icons and portraits (which could be extracted directly from the game's .pac files for display within the tool), or handling unit positioning in modified ENTD events.
- Identify linkages between Override tables and the data they are overriding, and make it easier to identify and modify overrides.

<img width="786" height="443" alt="image" src="https://github.com/user-attachments/assets/a07ed2e5-1f8b-4d8e-8c19-bec0f4f4ddf6" />

<img width="786" height="443" alt="image" src="https://github.com/user-attachments/assets/078b504f-36a0-49e4-9ddb-26f8b8619562" />

<img width="786" height="443" alt="image" src="https://github.com/user-attachments/assets/fd40f51c-0ea7-4d54-a34b-980f296b3ccf" />
