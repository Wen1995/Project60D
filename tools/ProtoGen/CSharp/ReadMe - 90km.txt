2017/12/11 By fireball
- 屏蔽clientprotobuf.css的警告
- UmbrellaClassGenerator.cs line.137 line 143
- writer.WriteLine("#pragma warning restore");
- //writer.WriteLine("#pragma warning disable 1591, 0612, 3021");
- writer.WriteLine("#pragma warning disable");

- 对所有的FiledNumber增加属性，有助于在生成lua wraper的时候过滤 [-] 删除global::System.Diagnostics.DebuggerNonUserCodeAttribute()
- ProtoGen/ExtensionGenerator.cs
- ProtoGen/MessageGenerator.cs
- ProtoGen/ServiceGenerator.cs
- ServiceInterfaceGenerator.cs
- UmbrellaClassGenerator.cs

- 在一些特定的代码处增加对unity editor的宏，屏蔽在发布版不需要的信息