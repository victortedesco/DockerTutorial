# Automatically Use `wsl docker` When Typing `docker` in PowerShell

To make the `docker` command in PowerShell automatically run `wsl docker`, you can create a function or alias that replaces `docker` with `wsl docker`. In PowerShell, this can be done by adding an alias or function to your PowerShell profile.

Here’s how to do it:

## 1. Create the PowerShell Profile File

Open PowerShell and run the following command to create the profile file:

```powershell
New-Item -Path $PROFILE -ItemType File -Force
```

This will create the profile file at the correct path. The `-Force` parameter ensures that the file is created if it does not already exist.

## 2. Edit the PowerShell Profile

After creating the file, open it with Notepad:

```powershell
notepad $PROFILE
```

## 3. Add the Function to Redirect `docker` Commands

In the text editor that opened, add the following code to the file:

```powershell
function docker {
    wsl docker $args
}
```

This creates a function named `docker` that will replace `docker` commands with the equivalent `wsl docker` commands.

## 4. Save and Close the Editor

Save the file and close Notepad.

## 5. Reload the Profile

To reload the PowerShell profile and apply the changes, run the following command:

```powershell
. $PROFILE
```

## 6. Test the New Command

Now, type `docker --version` or any other `docker` command in PowerShell. It should be redirected to `wsl docker`.

If everything is set up correctly, you will see that the `docker` command is being executed via WSL.
