﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TokenApp {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "12.0.0.0")]
    public sealed partial class TokenSetting : global::System.Configuration.ApplicationSettingsBase {
        
        private static TokenSetting defaultInstance = ((TokenSetting)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new TokenSetting())));
        
        public static TokenSetting Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Malebolgia1992")]
        public string SecurityKey {
            get {
                return ((string)(this["SecurityKey"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://www.spawn.com")]
        public string AudienceAddress {
            get {
                return ((string)(this["AudienceAddress"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("TokenOne")]
        public string TokenIssuerName {
            get {
                return ((string)(this["TokenIssuerName"]));
            }
        }
    }
}
