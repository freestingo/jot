# Jot

This program exports all your Kobo annotations into a list of organized, formatted text files. This lets you freely process your writings however you want (i.e. easier viewing when studying, creating and storing backups, importing your notes into other knowledge-base applications such as Notion or Obsidian, etc.).

## Example output

You can [download the example output here](https://github.com/user-attachments/files/24778644/annotations_2026-01-21.zip) to see how the generated files look like.

## How to use Jot

1. Locate the latest release from the [release page](https://github.com/freestingo/jot/releases) and download the archive file corresponding to your computer's architecture. For example, if you are using a Windows machine, you will most likely just need to download the **win-x64.zip** file. Unzip it anywhere and open it; this is what the folder will look like:
```
 📁 win-x64
 ├── 💿 Jot.exe
 ├── 🔧 Jot.pdb
 └── 📄 README.md
```
2. Connect your Kobo eReader to your computer. Open its drive and navigate to the `.kobo` subfolder (you might need to activate hidden folder and files viewing in your file explorer in order to locate it). Copy the `KoboReader.sqlite` database file and paste it in the folder you extracted in the previous step, which should now look something like this:  
```diff
 📁 win-x64
 ├── 💿 Jot.exe
 ├── 🔧 Jot.pdb
 ├── 📄 README.md
+└── 💾 KoboReader.sqlite
```
3. Double-click and run the `Jot.exe` program. You will find your annotations in a new subfolder called `annotations_{current-date}`, as shown in the following example:
```diff
 📁 win-x64
 ├── 💿 Jot.exe
 ├── 🔧 Jot.pdb
 ├── 📄 README.md
 ├── 💾 KoboReader.sqlite
+└── 📁 annotations_2026-01-21
+    ├── 📑 Cognitive Load_ Expectation Violation and Mild Despair_ A Neurobehavioral Study of Reading Documentation - L_ M_ Hartwell.md
+    └── 📑 The Observed Reader_ A Comprehensive Study of You Currently Reading This - A_ N_ Other.md
```
