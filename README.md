# BB_Catalogs
Make sure to run the generateDB.SQL file first before compiling...otherwise bad things may happend.
You will then have to update your web.config in the BB_CatalogManager and app.config in the mdl project. 
This is required. Entity Framework is having a tough time with the circular reference to catalog parentId. 
