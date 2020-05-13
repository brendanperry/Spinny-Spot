using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.iOS.Xcode;
using System.IO;
using System;

public class PostBuildScripts {

    [PostProcessBuild]
    public static void AddPhotoLibraryUsageKey(BuildTarget buildTarget, string pathToBuiltProject) {

        if (buildTarget == BuildTarget.iOS) {

            // Get plist
            string plistPath = pathToBuiltProject + "/Info.plist";
            PlistDocument plist = new PlistDocument();
            plist.ReadFromString(File.ReadAllText(plistPath));

            // Get root
            PlistElementDict rootDict = plist.root;

            // Ensure that photo usage description is always added
            var buildKey = "NSPhotoLibraryUsageDescription";
            rootDict.SetString(buildKey, "Photo library access needed to save screenshots");

            // Write to file
            File.WriteAllText(plistPath, plist.WriteToString());
        }
    }

    [PostProcessBuild]
    public static void AddCalenderUsageKey(BuildTarget buildTarget, string pathToBuiltProject) {

        if (buildTarget == BuildTarget.iOS) {

            // Get plist
            string plistPath = pathToBuiltProject + "/Info.plist";
            PlistDocument plist = new PlistDocument();
            plist.ReadFromString(File.ReadAllText(plistPath));

            // Get root
            PlistElementDict rootDict = plist.root;

            // Ensure that photo usage description is always added
            var buildKey = "NSCalendarsUsageDescription";
            rootDict.SetString(buildKey, "Advertisement would like to create a calender event");

            // Write to file
            File.WriteAllText(plistPath, plist.WriteToString());
        }
    }

    [PostProcessBuild]
    public static void AddBluetoothUsageKey(BuildTarget buildTarget, string pathToBuiltProject) {

        if (buildTarget == BuildTarget.iOS) {

            // Get plist
            string plistPath = pathToBuiltProject + "/Info.plist";
            PlistDocument plist = new PlistDocument();
            plist.ReadFromString(File.ReadAllText(plistPath));

            // Get root
            PlistElementDict rootDict = plist.root;

            // Ensure that photo usage description is always added
            var buildKey = "NSBluetoothPeripheralUsageDescription";
            rootDict.SetString(buildKey, "Advertisement would like to use bluetooth");

            // Write to file
            File.WriteAllText(plistPath, plist.WriteToString());
        }
    }

    [PostProcessBuild]
    public static void DisableAllowArbitraryLoads(BuildTarget buildTarget, string pathToBuiltProject) {
        if(buildTarget == BuildTarget.iOS) {
            // Get plist
            string plistPath = pathToBuiltProject + "/Info.plist";
            PlistDocument plist = new PlistDocument();
            plist.ReadFromString(File.ReadAllText(plistPath));

            // Get root
            PlistElementDict rootDict = plist.root;

            PlistElementDict appTransportSecurity = rootDict.CreateDict("NSAppTransportSecurity");

            // enable app transport
            appTransportSecurity.SetBoolean("NSAllowsArbitraryLoads", false);

            // Write to file
            File.WriteAllText(plistPath, plist.WriteToString());
        }
    }

    [PostProcessBuildAttribute(1)]
    public static void AddUserNotifications(BuildTarget buildTarget, string path) {
        if (buildTarget == BuildTarget.iOS) {
            // Read.
            string projectPath = PBXProject.GetPBXProjectPath(path);
            PBXProject project = new PBXProject();
            project.ReadFromString(File.ReadAllText(projectPath));
            string targetName = PBXProject.GetUnityTargetName();
            string projectTarget = project.TargetGuidByName(targetName);

            AddFrameworks(project, projectTarget);

            // Write.
            File.WriteAllText(projectPath, project.WriteToString());
        }
    }

    static void AddFrameworks(PBXProject project, string target) {
        // Frameworks (eppz! Photos, Google Analytics).
        project.AddFrameworkToProject(target, "UserNotifications.framework", false);

        // Add `-ObjC` to "Other Linker Flags".
        project.AddBuildProperty(target, "OTHER_LDFLAGS", "-ObjC");
    }
/*
    [PostProcessBuild(1500)]
    public static void GoogleAdsXcodeSupport(BuildTarget target, string path) {
#if UNITY_IPHONE
        if (target != BuildTarget.iOS) return;

        //string buildName = Path.GetFileNameWithoutExtension(path);
        string pbxprojPath = path + "/Unity-iPhone.xcodeproj/project.pbxproj";

        PBXProject proj = new PBXProject();

        proj.ReadFromString(File.ReadAllText(pbxprojPath));

        string buildTarget = proj.TargetGuidByName("Unity-iPhone");

        //DirectoryInfo projectParent = Directory.GetParent(Application.dataPath);

        char divider = Path.DirectorySeparatorChar;

        DirectoryInfo destinationFolder =
          new DirectoryInfo(path + divider + "Frameworks");

        foreach (DirectoryInfo file in destinationFolder.GetDirectories()) {
            string filePath = "Frameworks/" + file.Name;
            proj.AddFile(filePath, filePath, PBXSourceTree.Source);
            proj.AddFrameworkToProject(buildTarget, file.Name, false);
        }

        proj.SetBuildProperty(
          buildTarget, "FRAMEWORK_SEARCH_PATHS", "$(SRCROOT)/Frameworks"
        );
        proj.AddBuildProperty(
          buildTarget, "FRAMEWORK_SEARCH_PATHS", "$(inherited)"
        );
        proj.SetBuildProperty(
          buildTarget, "CLANG_ENABLE_MODULES", "YES"
        );

        File.WriteAllText(pbxprojPath, proj.WriteToString());
#endif
    }
    */


    [PostProcessBuild(1500)] // We should try to run last
    public static void AddEnableModules(BuildTarget target, string path) {
#if UNITY_IPHONE
        if (target != BuildTarget.iOS) {
            return; // How did we get here?
        }

        UnityEngine.Debug.Log("ScarletPostProcessor: Enabling Objective-C modules");
        string pbxproj = path + "/Unity-iPhone.xcodeproj/project.pbxproj";

        // Looking for the buildSettings sections of the pbxproj
        string insertKeyword = "buildSettings = {";
        string foundKeyword = "CLANG_ENABLE_MODULES"; // for checking if it's already inserted
        string modulesFlag = "				CLANG_ENABLE_MODULES = YES;";

        List<string> lines = new List<string>();

        foreach (string str in File.ReadAllLines(pbxproj)) {
            if (!str.Contains(foundKeyword)) {
                lines.Add(str);
            }
            if (str.Contains(insertKeyword)) {
                lines.Add(modulesFlag);
            }
        }
        
        using (File.Create(pbxproj)) { }

        foreach (string str in lines) {
            File.AppendAllText(pbxproj, str + Environment.NewLine);
        }

#endif
    }
}