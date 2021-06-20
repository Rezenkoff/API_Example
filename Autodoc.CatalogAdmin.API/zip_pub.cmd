del autodoc-catalog-admin.zip

cd bin\release\net5.0\ubuntu.18.04-x64

del autodoc-catalog-admin.zip

c:\!Deploy\bin\zip.exe  autodoc-catalog-admin.zip publish\*.* -r

cd ..\..\..\..\

move bin\release\net5.0\ubuntu.18.04-x64\autodoc-catalog-admin.zip %bamboo_build_working_directory%\autodoc-catalog-admin.zip


