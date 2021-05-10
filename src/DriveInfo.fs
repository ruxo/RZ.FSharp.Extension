module RZ.FSharp.Extension.DriveInfo

open System.IO

let inline availableFreeSpace (di: DriveInfo) = di.AvailableFreeSpace
let inline driveFormat (di: DriveInfo) = di.DriveFormat
let inline driveType (di: DriveInfo) = di.DriveType
let inline isReady (di: DriveInfo) = di.IsReady
let inline name (di: DriveInfo) = di.Name
let inline rootDirectory (di: DriveInfo) = di.RootDirectory
let inline totalFreeSpace (di: DriveInfo) = di.TotalFreeSpace
let inline totalSize (di: DriveInfo) = di.TotalSize
let inline volumeLabel (di: DriveInfo) = di.VolumeLabel
