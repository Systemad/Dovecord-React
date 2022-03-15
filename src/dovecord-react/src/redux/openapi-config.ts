import type { ConfigFile } from '@rtk-query/codegen-openapi'

const config: ConfigFile = {
    schemaFile: 'C:/Users/yeahg/RiderProjects/Dovecord-React/src/dovecord-react/src/services/swagger.json',
    apiFile: 'C:/Users/yeahg/RiderProjects/Dovecord-React/src/dovecord-react/src/redux/emptyApi.ts',
    apiImport: 'emptySplitApi',
    outputFile: 'C:/Users/yeahg/RiderProjects/Dovecord-React/src/dovecord-react/src/redux/webApi.ts',
    exportName: 'webApi',
    hooks: true,
}

export default config