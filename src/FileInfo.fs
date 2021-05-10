module RZ.FSharp.Extension.FileInfo

open System.IO

let inline directory (fi :FileInfo) = fi.Directory
let inline directoryName (fi :FileInfo) = fi.DirectoryName
let inline exists (fi :FileInfo) = fi.Exists
let inline isReadOnly (fi :FileInfo) = fi.IsReadOnly
let inline length (fi :FileInfo) = fi.Length
let inline name (fi :FileInfo) = fi.Name

let inline appendText (fi :FileInfo) = fi.AppendText()
let inline copyTo (fi :FileInfo) file_name = fi.CopyTo file_name
let inline copyTo2 (fi :FileInfo) overwrite file_name = fi.CopyTo(file_name, overwrite)
let inline create (fi :FileInfo) = fi.Create()
let inline createText (fi :FileInfo) = fi.CreateText()
let inline decrypt (fi :FileInfo) = fi.Decrypt()
let inline delete (fi :FileInfo) = fi.Delete()
let inline encrypt (fi :FileInfo) = fi.Encrypt()
let inline moveTo (fi :FileInfo) file_name = fi.MoveTo file_name
let inline moveTo2 (fi :FileInfo) overwrite file_name = fi.MoveTo(file_name, overwrite)
let inline ``open`` (fi :FileInfo) file_mode = fi.Open file_mode
let inline open2 (fi :FileInfo) file_mode access = fi.Open(file_mode, access)
let inline open3 (fi :FileInfo) file_mode access share = fi.Open(file_mode, access, share)
let inline openRead (fi :FileInfo) = fi.OpenRead()
let inline openText (fi :FileInfo) = fi.OpenText()
let inline openWrite (fi :FileInfo) = fi.OpenWrite()
let inline replace (fi :FileInfo) (filename, backup) = fi.Replace(filename, backup)
let inline replace2 (fi :FileInfo) ignore_error (filename, backup) = fi.Replace(filename, backup, ignore_error)
