// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: common.proto

package com.game.framework.protocol;

public final class Common {
  private Common() {}
  public static void registerAllExtensions(
      com.google.protobuf.ExtensionRegistry registry) {
  }
  /**
   * Protobuf enum {@code com.game.framework.protocol.Cmd}
   */
  public enum Cmd
      implements com.google.protobuf.ProtocolMessageEnum {
    /**
     * <code>ERROR = 0;</code>
     */
    ERROR(0, 0),
    /**
     * <code>LOGIN = 1;</code>
     *
     * <pre>
     *&#47;///////////////登录模块 1-10///////////////////
     * 登录
     * </pre>
     */
    LOGIN(1, 1),
    /**
     * <code>LOGOUT = 2;</code>
     *
     * <pre>
     * 玩家登出
     * </pre>
     */
    LOGOUT(2, 2),
    ;

    /**
     * <code>ERROR = 0;</code>
     */
    public static final int ERROR_VALUE = 0;
    /**
     * <code>LOGIN = 1;</code>
     *
     * <pre>
     *&#47;///////////////登录模块 1-10///////////////////
     * 登录
     * </pre>
     */
    public static final int LOGIN_VALUE = 1;
    /**
     * <code>LOGOUT = 2;</code>
     *
     * <pre>
     * 玩家登出
     * </pre>
     */
    public static final int LOGOUT_VALUE = 2;


    public final int getNumber() { return value; }

    public static Cmd valueOf(int value) {
      switch (value) {
        case 0: return ERROR;
        case 1: return LOGIN;
        case 2: return LOGOUT;
        default: return null;
      }
    }

    public static com.google.protobuf.Internal.EnumLiteMap<Cmd>
        internalGetValueMap() {
      return internalValueMap;
    }
    private static com.google.protobuf.Internal.EnumLiteMap<Cmd>
        internalValueMap =
          new com.google.protobuf.Internal.EnumLiteMap<Cmd>() {
            public Cmd findValueByNumber(int number) {
              return Cmd.valueOf(number);
            }
          };

    public final com.google.protobuf.Descriptors.EnumValueDescriptor
        getValueDescriptor() {
      return getDescriptor().getValues().get(index);
    }
    public final com.google.protobuf.Descriptors.EnumDescriptor
        getDescriptorForType() {
      return getDescriptor();
    }
    public static final com.google.protobuf.Descriptors.EnumDescriptor
        getDescriptor() {
      return com.game.framework.protocol.Common.getDescriptor().getEnumTypes().get(0);
    }

    private static final Cmd[] VALUES = values();

    public static Cmd valueOf(
        com.google.protobuf.Descriptors.EnumValueDescriptor desc) {
      if (desc.getType() != getDescriptor()) {
        throw new java.lang.IllegalArgumentException(
          "EnumValueDescriptor is not for this type.");
      }
      return VALUES[desc.getIndex()];
    }

    private final int index;
    private final int value;

    private Cmd(int index, int value) {
      this.index = index;
      this.value = value;
    }

    // @@protoc_insertion_point(enum_scope:com.game.framework.protocol.Cmd)
  }

  /**
   * Protobuf enum {@code com.game.framework.protocol.Error}
   */
  public enum Error
      implements com.google.protobuf.ProtocolMessageEnum {
    /**
     * <code>SERVER_ERR = 1;</code>
     *
     * <pre>
     * 服务器内部错误
     * </pre>
     */
    SERVER_ERR(0, 1),
    ;

    /**
     * <code>SERVER_ERR = 1;</code>
     *
     * <pre>
     * 服务器内部错误
     * </pre>
     */
    public static final int SERVER_ERR_VALUE = 1;


    public final int getNumber() { return value; }

    public static Error valueOf(int value) {
      switch (value) {
        case 1: return SERVER_ERR;
        default: return null;
      }
    }

    public static com.google.protobuf.Internal.EnumLiteMap<Error>
        internalGetValueMap() {
      return internalValueMap;
    }
    private static com.google.protobuf.Internal.EnumLiteMap<Error>
        internalValueMap =
          new com.google.protobuf.Internal.EnumLiteMap<Error>() {
            public Error findValueByNumber(int number) {
              return Error.valueOf(number);
            }
          };

    public final com.google.protobuf.Descriptors.EnumValueDescriptor
        getValueDescriptor() {
      return getDescriptor().getValues().get(index);
    }
    public final com.google.protobuf.Descriptors.EnumDescriptor
        getDescriptorForType() {
      return getDescriptor();
    }
    public static final com.google.protobuf.Descriptors.EnumDescriptor
        getDescriptor() {
      return com.game.framework.protocol.Common.getDescriptor().getEnumTypes().get(1);
    }

    private static final Error[] VALUES = values();

    public static Error valueOf(
        com.google.protobuf.Descriptors.EnumValueDescriptor desc) {
      if (desc.getType() != getDescriptor()) {
        throw new java.lang.IllegalArgumentException(
          "EnumValueDescriptor is not for this type.");
      }
      return VALUES[desc.getIndex()];
    }

    private final int index;
    private final int value;

    private Error(int index, int value) {
      this.index = index;
      this.value = value;
    }

    // @@protoc_insertion_point(enum_scope:com.game.framework.protocol.Error)
  }


  public static com.google.protobuf.Descriptors.FileDescriptor
      getDescriptor() {
    return descriptor;
  }
  private static com.google.protobuf.Descriptors.FileDescriptor
      descriptor;
  static {
    java.lang.String[] descriptorData = {
      "\n\014common.proto\022\033com.game.framework.proto" +
      "col*\'\n\003Cmd\022\t\n\005ERROR\020\000\022\t\n\005LOGIN\020\001\022\n\n\006LOGO" +
      "UT\020\002*\027\n\005Error\022\016\n\nSERVER_ERR\020\001B\002H\001"
    };
    com.google.protobuf.Descriptors.FileDescriptor.InternalDescriptorAssigner assigner =
      new com.google.protobuf.Descriptors.FileDescriptor.InternalDescriptorAssigner() {
        public com.google.protobuf.ExtensionRegistry assignDescriptors(
            com.google.protobuf.Descriptors.FileDescriptor root) {
          descriptor = root;
          return null;
        }
      };
    com.google.protobuf.Descriptors.FileDescriptor
      .internalBuildGeneratedFileFrom(descriptorData,
        new com.google.protobuf.Descriptors.FileDescriptor[] {
        }, assigner);
  }

  // @@protoc_insertion_point(outer_class_scope)
}
