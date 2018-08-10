// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: UserCache.proto

package com.game.framework.dbcache.model;

public final class UserCache {
  private UserCache() {}
  public static void registerAllExtensions(
      com.google.protobuf.ExtensionRegistry registry) {
  }
  public interface ProtoUserOrBuilder
      extends com.google.protobuf.MessageOrBuilder {

    // optional int64 id = 1;
    /**
     * <code>optional int64 id = 1;</code>
     */
    boolean hasId();
    /**
     * <code>optional int64 id = 1;</code>
     */
    long getId();

    // optional string account = 2;
    /**
     * <code>optional string account = 2;</code>
     */
    boolean hasAccount();
    /**
     * <code>optional string account = 2;</code>
     */
    java.lang.String getAccount();
    /**
     * <code>optional string account = 2;</code>
     */
    com.google.protobuf.ByteString
        getAccountBytes();

    // optional string password = 3;
    /**
     * <code>optional string password = 3;</code>
     */
    boolean hasPassword();
    /**
     * <code>optional string password = 3;</code>
     */
    java.lang.String getPassword();
    /**
     * <code>optional string password = 3;</code>
     */
    com.google.protobuf.ByteString
        getPasswordBytes();

    // optional int64 groupId = 4;
    /**
     * <code>optional int64 groupId = 4;</code>
     */
    boolean hasGroupId();
    /**
     * <code>optional int64 groupId = 4;</code>
     */
    long getGroupId();

    // optional int32 contribution = 5;
    /**
     * <code>optional int32 contribution = 5;</code>
     */
    boolean hasContribution();
    /**
     * <code>optional int32 contribution = 5;</code>
     */
    int getContribution();

    // optional int32 gold = 6;
    /**
     * <code>optional int32 gold = 6;</code>
     */
    boolean hasGold();
    /**
     * <code>optional int32 gold = 6;</code>
     */
    int getGold();

    // optional int32 production = 7;
    /**
     * <code>optional int32 production = 7;</code>
     */
    boolean hasProduction();
    /**
     * <code>optional int32 production = 7;</code>
     */
    int getProduction();

    // optional sint64 createTime = 8;
    /**
     * <code>optional sint64 createTime = 8;</code>
     */
    boolean hasCreateTime();
    /**
     * <code>optional sint64 createTime = 8;</code>
     */
    long getCreateTime();

    // optional bytes resource = 9;
    /**
     * <code>optional bytes resource = 9;</code>
     */
    boolean hasResource();
    /**
     * <code>optional bytes resource = 9;</code>
     */
    com.google.protobuf.ByteString getResource();
  }
  /**
   * Protobuf type {@code com.game.framework.dbcache.model.ProtoUser}
   */
  public static final class ProtoUser extends
      com.google.protobuf.GeneratedMessage
      implements ProtoUserOrBuilder {
    // Use ProtoUser.newBuilder() to construct.
    private ProtoUser(com.google.protobuf.GeneratedMessage.Builder<?> builder) {
      super(builder);
      this.unknownFields = builder.getUnknownFields();
    }
    private ProtoUser(boolean noInit) { this.unknownFields = com.google.protobuf.UnknownFieldSet.getDefaultInstance(); }

    private static final ProtoUser defaultInstance;
    public static ProtoUser getDefaultInstance() {
      return defaultInstance;
    }

    public ProtoUser getDefaultInstanceForType() {
      return defaultInstance;
    }

    private final com.google.protobuf.UnknownFieldSet unknownFields;
    @java.lang.Override
    public final com.google.protobuf.UnknownFieldSet
        getUnknownFields() {
      return this.unknownFields;
    }
    private ProtoUser(
        com.google.protobuf.CodedInputStream input,
        com.google.protobuf.ExtensionRegistryLite extensionRegistry)
        throws com.google.protobuf.InvalidProtocolBufferException {
      initFields();
      int mutable_bitField0_ = 0;
      com.google.protobuf.UnknownFieldSet.Builder unknownFields =
          com.google.protobuf.UnknownFieldSet.newBuilder();
      try {
        boolean done = false;
        while (!done) {
          int tag = input.readTag();
          switch (tag) {
            case 0:
              done = true;
              break;
            default: {
              if (!parseUnknownField(input, unknownFields,
                                     extensionRegistry, tag)) {
                done = true;
              }
              break;
            }
            case 8: {
              bitField0_ |= 0x00000001;
              id_ = input.readInt64();
              break;
            }
            case 18: {
              bitField0_ |= 0x00000002;
              account_ = input.readBytes();
              break;
            }
            case 26: {
              bitField0_ |= 0x00000004;
              password_ = input.readBytes();
              break;
            }
            case 32: {
              bitField0_ |= 0x00000008;
              groupId_ = input.readInt64();
              break;
            }
            case 40: {
              bitField0_ |= 0x00000010;
              contribution_ = input.readInt32();
              break;
            }
            case 48: {
              bitField0_ |= 0x00000020;
              gold_ = input.readInt32();
              break;
            }
            case 56: {
              bitField0_ |= 0x00000040;
              production_ = input.readInt32();
              break;
            }
            case 64: {
              bitField0_ |= 0x00000080;
              createTime_ = input.readSInt64();
              break;
            }
            case 74: {
              bitField0_ |= 0x00000100;
              resource_ = input.readBytes();
              break;
            }
          }
        }
      } catch (com.google.protobuf.InvalidProtocolBufferException e) {
        throw e.setUnfinishedMessage(this);
      } catch (java.io.IOException e) {
        throw new com.google.protobuf.InvalidProtocolBufferException(
            e.getMessage()).setUnfinishedMessage(this);
      } finally {
        this.unknownFields = unknownFields.build();
        makeExtensionsImmutable();
      }
    }
    public static final com.google.protobuf.Descriptors.Descriptor
        getDescriptor() {
      return com.game.framework.dbcache.model.UserCache.internal_static_com_game_framework_dbcache_model_ProtoUser_descriptor;
    }

    protected com.google.protobuf.GeneratedMessage.FieldAccessorTable
        internalGetFieldAccessorTable() {
      return com.game.framework.dbcache.model.UserCache.internal_static_com_game_framework_dbcache_model_ProtoUser_fieldAccessorTable
          .ensureFieldAccessorsInitialized(
              com.game.framework.dbcache.model.UserCache.ProtoUser.class, com.game.framework.dbcache.model.UserCache.ProtoUser.Builder.class);
    }

    public static com.google.protobuf.Parser<ProtoUser> PARSER =
        new com.google.protobuf.AbstractParser<ProtoUser>() {
      public ProtoUser parsePartialFrom(
          com.google.protobuf.CodedInputStream input,
          com.google.protobuf.ExtensionRegistryLite extensionRegistry)
          throws com.google.protobuf.InvalidProtocolBufferException {
        return new ProtoUser(input, extensionRegistry);
      }
    };

    @java.lang.Override
    public com.google.protobuf.Parser<ProtoUser> getParserForType() {
      return PARSER;
    }

    private int bitField0_;
    // optional int64 id = 1;
    public static final int ID_FIELD_NUMBER = 1;
    private long id_;
    /**
     * <code>optional int64 id = 1;</code>
     */
    public boolean hasId() {
      return ((bitField0_ & 0x00000001) == 0x00000001);
    }
    /**
     * <code>optional int64 id = 1;</code>
     */
    public long getId() {
      return id_;
    }

    // optional string account = 2;
    public static final int ACCOUNT_FIELD_NUMBER = 2;
    private java.lang.Object account_;
    /**
     * <code>optional string account = 2;</code>
     */
    public boolean hasAccount() {
      return ((bitField0_ & 0x00000002) == 0x00000002);
    }
    /**
     * <code>optional string account = 2;</code>
     */
    public java.lang.String getAccount() {
      java.lang.Object ref = account_;
      if (ref instanceof java.lang.String) {
        return (java.lang.String) ref;
      } else {
        com.google.protobuf.ByteString bs = 
            (com.google.protobuf.ByteString) ref;
        java.lang.String s = bs.toStringUtf8();
        if (bs.isValidUtf8()) {
          account_ = s;
        }
        return s;
      }
    }
    /**
     * <code>optional string account = 2;</code>
     */
    public com.google.protobuf.ByteString
        getAccountBytes() {
      java.lang.Object ref = account_;
      if (ref instanceof java.lang.String) {
        com.google.protobuf.ByteString b = 
            com.google.protobuf.ByteString.copyFromUtf8(
                (java.lang.String) ref);
        account_ = b;
        return b;
      } else {
        return (com.google.protobuf.ByteString) ref;
      }
    }

    // optional string password = 3;
    public static final int PASSWORD_FIELD_NUMBER = 3;
    private java.lang.Object password_;
    /**
     * <code>optional string password = 3;</code>
     */
    public boolean hasPassword() {
      return ((bitField0_ & 0x00000004) == 0x00000004);
    }
    /**
     * <code>optional string password = 3;</code>
     */
    public java.lang.String getPassword() {
      java.lang.Object ref = password_;
      if (ref instanceof java.lang.String) {
        return (java.lang.String) ref;
      } else {
        com.google.protobuf.ByteString bs = 
            (com.google.protobuf.ByteString) ref;
        java.lang.String s = bs.toStringUtf8();
        if (bs.isValidUtf8()) {
          password_ = s;
        }
        return s;
      }
    }
    /**
     * <code>optional string password = 3;</code>
     */
    public com.google.protobuf.ByteString
        getPasswordBytes() {
      java.lang.Object ref = password_;
      if (ref instanceof java.lang.String) {
        com.google.protobuf.ByteString b = 
            com.google.protobuf.ByteString.copyFromUtf8(
                (java.lang.String) ref);
        password_ = b;
        return b;
      } else {
        return (com.google.protobuf.ByteString) ref;
      }
    }

    // optional int64 groupId = 4;
    public static final int GROUPID_FIELD_NUMBER = 4;
    private long groupId_;
    /**
     * <code>optional int64 groupId = 4;</code>
     */
    public boolean hasGroupId() {
      return ((bitField0_ & 0x00000008) == 0x00000008);
    }
    /**
     * <code>optional int64 groupId = 4;</code>
     */
    public long getGroupId() {
      return groupId_;
    }

    // optional int32 contribution = 5;
    public static final int CONTRIBUTION_FIELD_NUMBER = 5;
    private int contribution_;
    /**
     * <code>optional int32 contribution = 5;</code>
     */
    public boolean hasContribution() {
      return ((bitField0_ & 0x00000010) == 0x00000010);
    }
    /**
     * <code>optional int32 contribution = 5;</code>
     */
    public int getContribution() {
      return contribution_;
    }

    // optional int32 gold = 6;
    public static final int GOLD_FIELD_NUMBER = 6;
    private int gold_;
    /**
     * <code>optional int32 gold = 6;</code>
     */
    public boolean hasGold() {
      return ((bitField0_ & 0x00000020) == 0x00000020);
    }
    /**
     * <code>optional int32 gold = 6;</code>
     */
    public int getGold() {
      return gold_;
    }

    // optional int32 production = 7;
    public static final int PRODUCTION_FIELD_NUMBER = 7;
    private int production_;
    /**
     * <code>optional int32 production = 7;</code>
     */
    public boolean hasProduction() {
      return ((bitField0_ & 0x00000040) == 0x00000040);
    }
    /**
     * <code>optional int32 production = 7;</code>
     */
    public int getProduction() {
      return production_;
    }

    // optional sint64 createTime = 8;
    public static final int CREATETIME_FIELD_NUMBER = 8;
    private long createTime_;
    /**
     * <code>optional sint64 createTime = 8;</code>
     */
    public boolean hasCreateTime() {
      return ((bitField0_ & 0x00000080) == 0x00000080);
    }
    /**
     * <code>optional sint64 createTime = 8;</code>
     */
    public long getCreateTime() {
      return createTime_;
    }

    // optional bytes resource = 9;
    public static final int RESOURCE_FIELD_NUMBER = 9;
    private com.google.protobuf.ByteString resource_;
    /**
     * <code>optional bytes resource = 9;</code>
     */
    public boolean hasResource() {
      return ((bitField0_ & 0x00000100) == 0x00000100);
    }
    /**
     * <code>optional bytes resource = 9;</code>
     */
    public com.google.protobuf.ByteString getResource() {
      return resource_;
    }

    private void initFields() {
      id_ = 0L;
      account_ = "";
      password_ = "";
      groupId_ = 0L;
      contribution_ = 0;
      gold_ = 0;
      production_ = 0;
      createTime_ = 0L;
      resource_ = com.google.protobuf.ByteString.EMPTY;
    }
    private byte memoizedIsInitialized = -1;
    public final boolean isInitialized() {
      byte isInitialized = memoizedIsInitialized;
      if (isInitialized != -1) return isInitialized == 1;

      memoizedIsInitialized = 1;
      return true;
    }

    public void writeTo(com.google.protobuf.CodedOutputStream output)
                        throws java.io.IOException {
      getSerializedSize();
      if (((bitField0_ & 0x00000001) == 0x00000001)) {
        output.writeInt64(1, id_);
      }
      if (((bitField0_ & 0x00000002) == 0x00000002)) {
        output.writeBytes(2, getAccountBytes());
      }
      if (((bitField0_ & 0x00000004) == 0x00000004)) {
        output.writeBytes(3, getPasswordBytes());
      }
      if (((bitField0_ & 0x00000008) == 0x00000008)) {
        output.writeInt64(4, groupId_);
      }
      if (((bitField0_ & 0x00000010) == 0x00000010)) {
        output.writeInt32(5, contribution_);
      }
      if (((bitField0_ & 0x00000020) == 0x00000020)) {
        output.writeInt32(6, gold_);
      }
      if (((bitField0_ & 0x00000040) == 0x00000040)) {
        output.writeInt32(7, production_);
      }
      if (((bitField0_ & 0x00000080) == 0x00000080)) {
        output.writeSInt64(8, createTime_);
      }
      if (((bitField0_ & 0x00000100) == 0x00000100)) {
        output.writeBytes(9, resource_);
      }
      getUnknownFields().writeTo(output);
    }

    private int memoizedSerializedSize = -1;
    public int getSerializedSize() {
      int size = memoizedSerializedSize;
      if (size != -1) return size;

      size = 0;
      if (((bitField0_ & 0x00000001) == 0x00000001)) {
        size += com.google.protobuf.CodedOutputStream
          .computeInt64Size(1, id_);
      }
      if (((bitField0_ & 0x00000002) == 0x00000002)) {
        size += com.google.protobuf.CodedOutputStream
          .computeBytesSize(2, getAccountBytes());
      }
      if (((bitField0_ & 0x00000004) == 0x00000004)) {
        size += com.google.protobuf.CodedOutputStream
          .computeBytesSize(3, getPasswordBytes());
      }
      if (((bitField0_ & 0x00000008) == 0x00000008)) {
        size += com.google.protobuf.CodedOutputStream
          .computeInt64Size(4, groupId_);
      }
      if (((bitField0_ & 0x00000010) == 0x00000010)) {
        size += com.google.protobuf.CodedOutputStream
          .computeInt32Size(5, contribution_);
      }
      if (((bitField0_ & 0x00000020) == 0x00000020)) {
        size += com.google.protobuf.CodedOutputStream
          .computeInt32Size(6, gold_);
      }
      if (((bitField0_ & 0x00000040) == 0x00000040)) {
        size += com.google.protobuf.CodedOutputStream
          .computeInt32Size(7, production_);
      }
      if (((bitField0_ & 0x00000080) == 0x00000080)) {
        size += com.google.protobuf.CodedOutputStream
          .computeSInt64Size(8, createTime_);
      }
      if (((bitField0_ & 0x00000100) == 0x00000100)) {
        size += com.google.protobuf.CodedOutputStream
          .computeBytesSize(9, resource_);
      }
      size += getUnknownFields().getSerializedSize();
      memoizedSerializedSize = size;
      return size;
    }

    private static final long serialVersionUID = 0L;
    @java.lang.Override
    protected java.lang.Object writeReplace()
        throws java.io.ObjectStreamException {
      return super.writeReplace();
    }

    public static com.game.framework.dbcache.model.UserCache.ProtoUser parseFrom(
        com.google.protobuf.ByteString data)
        throws com.google.protobuf.InvalidProtocolBufferException {
      return PARSER.parseFrom(data);
    }
    public static com.game.framework.dbcache.model.UserCache.ProtoUser parseFrom(
        com.google.protobuf.ByteString data,
        com.google.protobuf.ExtensionRegistryLite extensionRegistry)
        throws com.google.protobuf.InvalidProtocolBufferException {
      return PARSER.parseFrom(data, extensionRegistry);
    }
    public static com.game.framework.dbcache.model.UserCache.ProtoUser parseFrom(byte[] data)
        throws com.google.protobuf.InvalidProtocolBufferException {
      return PARSER.parseFrom(data);
    }
    public static com.game.framework.dbcache.model.UserCache.ProtoUser parseFrom(
        byte[] data,
        com.google.protobuf.ExtensionRegistryLite extensionRegistry)
        throws com.google.protobuf.InvalidProtocolBufferException {
      return PARSER.parseFrom(data, extensionRegistry);
    }
    public static com.game.framework.dbcache.model.UserCache.ProtoUser parseFrom(java.io.InputStream input)
        throws java.io.IOException {
      return PARSER.parseFrom(input);
    }
    public static com.game.framework.dbcache.model.UserCache.ProtoUser parseFrom(
        java.io.InputStream input,
        com.google.protobuf.ExtensionRegistryLite extensionRegistry)
        throws java.io.IOException {
      return PARSER.parseFrom(input, extensionRegistry);
    }
    public static com.game.framework.dbcache.model.UserCache.ProtoUser parseDelimitedFrom(java.io.InputStream input)
        throws java.io.IOException {
      return PARSER.parseDelimitedFrom(input);
    }
    public static com.game.framework.dbcache.model.UserCache.ProtoUser parseDelimitedFrom(
        java.io.InputStream input,
        com.google.protobuf.ExtensionRegistryLite extensionRegistry)
        throws java.io.IOException {
      return PARSER.parseDelimitedFrom(input, extensionRegistry);
    }
    public static com.game.framework.dbcache.model.UserCache.ProtoUser parseFrom(
        com.google.protobuf.CodedInputStream input)
        throws java.io.IOException {
      return PARSER.parseFrom(input);
    }
    public static com.game.framework.dbcache.model.UserCache.ProtoUser parseFrom(
        com.google.protobuf.CodedInputStream input,
        com.google.protobuf.ExtensionRegistryLite extensionRegistry)
        throws java.io.IOException {
      return PARSER.parseFrom(input, extensionRegistry);
    }

    public static Builder newBuilder() { return Builder.create(); }
    public Builder newBuilderForType() { return newBuilder(); }
    public static Builder newBuilder(com.game.framework.dbcache.model.UserCache.ProtoUser prototype) {
      return newBuilder().mergeFrom(prototype);
    }
    public Builder toBuilder() { return newBuilder(this); }

    @java.lang.Override
    protected Builder newBuilderForType(
        com.google.protobuf.GeneratedMessage.BuilderParent parent) {
      Builder builder = new Builder(parent);
      return builder;
    }
    /**
     * Protobuf type {@code com.game.framework.dbcache.model.ProtoUser}
     */
    public static final class Builder extends
        com.google.protobuf.GeneratedMessage.Builder<Builder>
       implements com.game.framework.dbcache.model.UserCache.ProtoUserOrBuilder {
      public static final com.google.protobuf.Descriptors.Descriptor
          getDescriptor() {
        return com.game.framework.dbcache.model.UserCache.internal_static_com_game_framework_dbcache_model_ProtoUser_descriptor;
      }

      protected com.google.protobuf.GeneratedMessage.FieldAccessorTable
          internalGetFieldAccessorTable() {
        return com.game.framework.dbcache.model.UserCache.internal_static_com_game_framework_dbcache_model_ProtoUser_fieldAccessorTable
            .ensureFieldAccessorsInitialized(
                com.game.framework.dbcache.model.UserCache.ProtoUser.class, com.game.framework.dbcache.model.UserCache.ProtoUser.Builder.class);
      }

      // Construct using com.game.framework.dbcache.model.UserCache.ProtoUser.newBuilder()
      private Builder() {
        maybeForceBuilderInitialization();
      }

      private Builder(
          com.google.protobuf.GeneratedMessage.BuilderParent parent) {
        super(parent);
        maybeForceBuilderInitialization();
      }
      private void maybeForceBuilderInitialization() {
        if (com.google.protobuf.GeneratedMessage.alwaysUseFieldBuilders) {
        }
      }
      private static Builder create() {
        return new Builder();
      }

      public Builder clear() {
        super.clear();
        id_ = 0L;
        bitField0_ = (bitField0_ & ~0x00000001);
        account_ = "";
        bitField0_ = (bitField0_ & ~0x00000002);
        password_ = "";
        bitField0_ = (bitField0_ & ~0x00000004);
        groupId_ = 0L;
        bitField0_ = (bitField0_ & ~0x00000008);
        contribution_ = 0;
        bitField0_ = (bitField0_ & ~0x00000010);
        gold_ = 0;
        bitField0_ = (bitField0_ & ~0x00000020);
        production_ = 0;
        bitField0_ = (bitField0_ & ~0x00000040);
        createTime_ = 0L;
        bitField0_ = (bitField0_ & ~0x00000080);
        resource_ = com.google.protobuf.ByteString.EMPTY;
        bitField0_ = (bitField0_ & ~0x00000100);
        return this;
      }

      public Builder clone() {
        return create().mergeFrom(buildPartial());
      }

      public com.google.protobuf.Descriptors.Descriptor
          getDescriptorForType() {
        return com.game.framework.dbcache.model.UserCache.internal_static_com_game_framework_dbcache_model_ProtoUser_descriptor;
      }

      public com.game.framework.dbcache.model.UserCache.ProtoUser getDefaultInstanceForType() {
        return com.game.framework.dbcache.model.UserCache.ProtoUser.getDefaultInstance();
      }

      public com.game.framework.dbcache.model.UserCache.ProtoUser build() {
        com.game.framework.dbcache.model.UserCache.ProtoUser result = buildPartial();
        if (!result.isInitialized()) {
          throw newUninitializedMessageException(result);
        }
        return result;
      }

      public com.game.framework.dbcache.model.UserCache.ProtoUser buildPartial() {
        com.game.framework.dbcache.model.UserCache.ProtoUser result = new com.game.framework.dbcache.model.UserCache.ProtoUser(this);
        int from_bitField0_ = bitField0_;
        int to_bitField0_ = 0;
        if (((from_bitField0_ & 0x00000001) == 0x00000001)) {
          to_bitField0_ |= 0x00000001;
        }
        result.id_ = id_;
        if (((from_bitField0_ & 0x00000002) == 0x00000002)) {
          to_bitField0_ |= 0x00000002;
        }
        result.account_ = account_;
        if (((from_bitField0_ & 0x00000004) == 0x00000004)) {
          to_bitField0_ |= 0x00000004;
        }
        result.password_ = password_;
        if (((from_bitField0_ & 0x00000008) == 0x00000008)) {
          to_bitField0_ |= 0x00000008;
        }
        result.groupId_ = groupId_;
        if (((from_bitField0_ & 0x00000010) == 0x00000010)) {
          to_bitField0_ |= 0x00000010;
        }
        result.contribution_ = contribution_;
        if (((from_bitField0_ & 0x00000020) == 0x00000020)) {
          to_bitField0_ |= 0x00000020;
        }
        result.gold_ = gold_;
        if (((from_bitField0_ & 0x00000040) == 0x00000040)) {
          to_bitField0_ |= 0x00000040;
        }
        result.production_ = production_;
        if (((from_bitField0_ & 0x00000080) == 0x00000080)) {
          to_bitField0_ |= 0x00000080;
        }
        result.createTime_ = createTime_;
        if (((from_bitField0_ & 0x00000100) == 0x00000100)) {
          to_bitField0_ |= 0x00000100;
        }
        result.resource_ = resource_;
        result.bitField0_ = to_bitField0_;
        onBuilt();
        return result;
      }

      public Builder mergeFrom(com.google.protobuf.Message other) {
        if (other instanceof com.game.framework.dbcache.model.UserCache.ProtoUser) {
          return mergeFrom((com.game.framework.dbcache.model.UserCache.ProtoUser)other);
        } else {
          super.mergeFrom(other);
          return this;
        }
      }

      public Builder mergeFrom(com.game.framework.dbcache.model.UserCache.ProtoUser other) {
        if (other == com.game.framework.dbcache.model.UserCache.ProtoUser.getDefaultInstance()) return this;
        if (other.hasId()) {
          setId(other.getId());
        }
        if (other.hasAccount()) {
          bitField0_ |= 0x00000002;
          account_ = other.account_;
          onChanged();
        }
        if (other.hasPassword()) {
          bitField0_ |= 0x00000004;
          password_ = other.password_;
          onChanged();
        }
        if (other.hasGroupId()) {
          setGroupId(other.getGroupId());
        }
        if (other.hasContribution()) {
          setContribution(other.getContribution());
        }
        if (other.hasGold()) {
          setGold(other.getGold());
        }
        if (other.hasProduction()) {
          setProduction(other.getProduction());
        }
        if (other.hasCreateTime()) {
          setCreateTime(other.getCreateTime());
        }
        if (other.hasResource()) {
          setResource(other.getResource());
        }
        this.mergeUnknownFields(other.getUnknownFields());
        return this;
      }

      public final boolean isInitialized() {
        return true;
      }

      public Builder mergeFrom(
          com.google.protobuf.CodedInputStream input,
          com.google.protobuf.ExtensionRegistryLite extensionRegistry)
          throws java.io.IOException {
        com.game.framework.dbcache.model.UserCache.ProtoUser parsedMessage = null;
        try {
          parsedMessage = PARSER.parsePartialFrom(input, extensionRegistry);
        } catch (com.google.protobuf.InvalidProtocolBufferException e) {
          parsedMessage = (com.game.framework.dbcache.model.UserCache.ProtoUser) e.getUnfinishedMessage();
          throw e;
        } finally {
          if (parsedMessage != null) {
            mergeFrom(parsedMessage);
          }
        }
        return this;
      }
      private int bitField0_;

      // optional int64 id = 1;
      private long id_ ;
      /**
       * <code>optional int64 id = 1;</code>
       */
      public boolean hasId() {
        return ((bitField0_ & 0x00000001) == 0x00000001);
      }
      /**
       * <code>optional int64 id = 1;</code>
       */
      public long getId() {
        return id_;
      }
      /**
       * <code>optional int64 id = 1;</code>
       */
      public Builder setId(long value) {
        bitField0_ |= 0x00000001;
        id_ = value;
        onChanged();
        return this;
      }
      /**
       * <code>optional int64 id = 1;</code>
       */
      public Builder clearId() {
        bitField0_ = (bitField0_ & ~0x00000001);
        id_ = 0L;
        onChanged();
        return this;
      }

      // optional string account = 2;
      private java.lang.Object account_ = "";
      /**
       * <code>optional string account = 2;</code>
       */
      public boolean hasAccount() {
        return ((bitField0_ & 0x00000002) == 0x00000002);
      }
      /**
       * <code>optional string account = 2;</code>
       */
      public java.lang.String getAccount() {
        java.lang.Object ref = account_;
        if (!(ref instanceof java.lang.String)) {
          java.lang.String s = ((com.google.protobuf.ByteString) ref)
              .toStringUtf8();
          account_ = s;
          return s;
        } else {
          return (java.lang.String) ref;
        }
      }
      /**
       * <code>optional string account = 2;</code>
       */
      public com.google.protobuf.ByteString
          getAccountBytes() {
        java.lang.Object ref = account_;
        if (ref instanceof String) {
          com.google.protobuf.ByteString b = 
              com.google.protobuf.ByteString.copyFromUtf8(
                  (java.lang.String) ref);
          account_ = b;
          return b;
        } else {
          return (com.google.protobuf.ByteString) ref;
        }
      }
      /**
       * <code>optional string account = 2;</code>
       */
      public Builder setAccount(
          java.lang.String value) {
        if (value == null) {
    throw new NullPointerException();
  }
  bitField0_ |= 0x00000002;
        account_ = value;
        onChanged();
        return this;
      }
      /**
       * <code>optional string account = 2;</code>
       */
      public Builder clearAccount() {
        bitField0_ = (bitField0_ & ~0x00000002);
        account_ = getDefaultInstance().getAccount();
        onChanged();
        return this;
      }
      /**
       * <code>optional string account = 2;</code>
       */
      public Builder setAccountBytes(
          com.google.protobuf.ByteString value) {
        if (value == null) {
    throw new NullPointerException();
  }
  bitField0_ |= 0x00000002;
        account_ = value;
        onChanged();
        return this;
      }

      // optional string password = 3;
      private java.lang.Object password_ = "";
      /**
       * <code>optional string password = 3;</code>
       */
      public boolean hasPassword() {
        return ((bitField0_ & 0x00000004) == 0x00000004);
      }
      /**
       * <code>optional string password = 3;</code>
       */
      public java.lang.String getPassword() {
        java.lang.Object ref = password_;
        if (!(ref instanceof java.lang.String)) {
          java.lang.String s = ((com.google.protobuf.ByteString) ref)
              .toStringUtf8();
          password_ = s;
          return s;
        } else {
          return (java.lang.String) ref;
        }
      }
      /**
       * <code>optional string password = 3;</code>
       */
      public com.google.protobuf.ByteString
          getPasswordBytes() {
        java.lang.Object ref = password_;
        if (ref instanceof String) {
          com.google.protobuf.ByteString b = 
              com.google.protobuf.ByteString.copyFromUtf8(
                  (java.lang.String) ref);
          password_ = b;
          return b;
        } else {
          return (com.google.protobuf.ByteString) ref;
        }
      }
      /**
       * <code>optional string password = 3;</code>
       */
      public Builder setPassword(
          java.lang.String value) {
        if (value == null) {
    throw new NullPointerException();
  }
  bitField0_ |= 0x00000004;
        password_ = value;
        onChanged();
        return this;
      }
      /**
       * <code>optional string password = 3;</code>
       */
      public Builder clearPassword() {
        bitField0_ = (bitField0_ & ~0x00000004);
        password_ = getDefaultInstance().getPassword();
        onChanged();
        return this;
      }
      /**
       * <code>optional string password = 3;</code>
       */
      public Builder setPasswordBytes(
          com.google.protobuf.ByteString value) {
        if (value == null) {
    throw new NullPointerException();
  }
  bitField0_ |= 0x00000004;
        password_ = value;
        onChanged();
        return this;
      }

      // optional int64 groupId = 4;
      private long groupId_ ;
      /**
       * <code>optional int64 groupId = 4;</code>
       */
      public boolean hasGroupId() {
        return ((bitField0_ & 0x00000008) == 0x00000008);
      }
      /**
       * <code>optional int64 groupId = 4;</code>
       */
      public long getGroupId() {
        return groupId_;
      }
      /**
       * <code>optional int64 groupId = 4;</code>
       */
      public Builder setGroupId(long value) {
        bitField0_ |= 0x00000008;
        groupId_ = value;
        onChanged();
        return this;
      }
      /**
       * <code>optional int64 groupId = 4;</code>
       */
      public Builder clearGroupId() {
        bitField0_ = (bitField0_ & ~0x00000008);
        groupId_ = 0L;
        onChanged();
        return this;
      }

      // optional int32 contribution = 5;
      private int contribution_ ;
      /**
       * <code>optional int32 contribution = 5;</code>
       */
      public boolean hasContribution() {
        return ((bitField0_ & 0x00000010) == 0x00000010);
      }
      /**
       * <code>optional int32 contribution = 5;</code>
       */
      public int getContribution() {
        return contribution_;
      }
      /**
       * <code>optional int32 contribution = 5;</code>
       */
      public Builder setContribution(int value) {
        bitField0_ |= 0x00000010;
        contribution_ = value;
        onChanged();
        return this;
      }
      /**
       * <code>optional int32 contribution = 5;</code>
       */
      public Builder clearContribution() {
        bitField0_ = (bitField0_ & ~0x00000010);
        contribution_ = 0;
        onChanged();
        return this;
      }

      // optional int32 gold = 6;
      private int gold_ ;
      /**
       * <code>optional int32 gold = 6;</code>
       */
      public boolean hasGold() {
        return ((bitField0_ & 0x00000020) == 0x00000020);
      }
      /**
       * <code>optional int32 gold = 6;</code>
       */
      public int getGold() {
        return gold_;
      }
      /**
       * <code>optional int32 gold = 6;</code>
       */
      public Builder setGold(int value) {
        bitField0_ |= 0x00000020;
        gold_ = value;
        onChanged();
        return this;
      }
      /**
       * <code>optional int32 gold = 6;</code>
       */
      public Builder clearGold() {
        bitField0_ = (bitField0_ & ~0x00000020);
        gold_ = 0;
        onChanged();
        return this;
      }

      // optional int32 production = 7;
      private int production_ ;
      /**
       * <code>optional int32 production = 7;</code>
       */
      public boolean hasProduction() {
        return ((bitField0_ & 0x00000040) == 0x00000040);
      }
      /**
       * <code>optional int32 production = 7;</code>
       */
      public int getProduction() {
        return production_;
      }
      /**
       * <code>optional int32 production = 7;</code>
       */
      public Builder setProduction(int value) {
        bitField0_ |= 0x00000040;
        production_ = value;
        onChanged();
        return this;
      }
      /**
       * <code>optional int32 production = 7;</code>
       */
      public Builder clearProduction() {
        bitField0_ = (bitField0_ & ~0x00000040);
        production_ = 0;
        onChanged();
        return this;
      }

      // optional sint64 createTime = 8;
      private long createTime_ ;
      /**
       * <code>optional sint64 createTime = 8;</code>
       */
      public boolean hasCreateTime() {
        return ((bitField0_ & 0x00000080) == 0x00000080);
      }
      /**
       * <code>optional sint64 createTime = 8;</code>
       */
      public long getCreateTime() {
        return createTime_;
      }
      /**
       * <code>optional sint64 createTime = 8;</code>
       */
      public Builder setCreateTime(long value) {
        bitField0_ |= 0x00000080;
        createTime_ = value;
        onChanged();
        return this;
      }
      /**
       * <code>optional sint64 createTime = 8;</code>
       */
      public Builder clearCreateTime() {
        bitField0_ = (bitField0_ & ~0x00000080);
        createTime_ = 0L;
        onChanged();
        return this;
      }

      // optional bytes resource = 9;
      private com.google.protobuf.ByteString resource_ = com.google.protobuf.ByteString.EMPTY;
      /**
       * <code>optional bytes resource = 9;</code>
       */
      public boolean hasResource() {
        return ((bitField0_ & 0x00000100) == 0x00000100);
      }
      /**
       * <code>optional bytes resource = 9;</code>
       */
      public com.google.protobuf.ByteString getResource() {
        return resource_;
      }
      /**
       * <code>optional bytes resource = 9;</code>
       */
      public Builder setResource(com.google.protobuf.ByteString value) {
        if (value == null) {
    throw new NullPointerException();
  }
  bitField0_ |= 0x00000100;
        resource_ = value;
        onChanged();
        return this;
      }
      /**
       * <code>optional bytes resource = 9;</code>
       */
      public Builder clearResource() {
        bitField0_ = (bitField0_ & ~0x00000100);
        resource_ = getDefaultInstance().getResource();
        onChanged();
        return this;
      }

      // @@protoc_insertion_point(builder_scope:com.game.framework.dbcache.model.ProtoUser)
    }

    static {
      defaultInstance = new ProtoUser(true);
      defaultInstance.initFields();
    }

    // @@protoc_insertion_point(class_scope:com.game.framework.dbcache.model.ProtoUser)
  }

  private static com.google.protobuf.Descriptors.Descriptor
    internal_static_com_game_framework_dbcache_model_ProtoUser_descriptor;
  private static
    com.google.protobuf.GeneratedMessage.FieldAccessorTable
      internal_static_com_game_framework_dbcache_model_ProtoUser_fieldAccessorTable;

  public static com.google.protobuf.Descriptors.FileDescriptor
      getDescriptor() {
    return descriptor;
  }
  private static com.google.protobuf.Descriptors.FileDescriptor
      descriptor;
  static {
    java.lang.String[] descriptorData = {
      "\n\017UserCache.proto\022 com.game.framework.db" +
      "cache.model\"\251\001\n\tProtoUser\022\n\n\002id\030\001 \001(\003\022\017\n" +
      "\007account\030\002 \001(\t\022\020\n\010password\030\003 \001(\t\022\017\n\007grou" +
      "pId\030\004 \001(\003\022\024\n\014contribution\030\005 \001(\005\022\014\n\004gold\030" +
      "\006 \001(\005\022\022\n\nproduction\030\007 \001(\005\022\022\n\ncreateTime\030" +
      "\010 \001(\022\022\020\n\010resource\030\t \001(\014B\002H\001"
    };
    com.google.protobuf.Descriptors.FileDescriptor.InternalDescriptorAssigner assigner =
      new com.google.protobuf.Descriptors.FileDescriptor.InternalDescriptorAssigner() {
        public com.google.protobuf.ExtensionRegistry assignDescriptors(
            com.google.protobuf.Descriptors.FileDescriptor root) {
          descriptor = root;
          internal_static_com_game_framework_dbcache_model_ProtoUser_descriptor =
            getDescriptor().getMessageTypes().get(0);
          internal_static_com_game_framework_dbcache_model_ProtoUser_fieldAccessorTable = new
            com.google.protobuf.GeneratedMessage.FieldAccessorTable(
              internal_static_com_game_framework_dbcache_model_ProtoUser_descriptor,
              new java.lang.String[] { "Id", "Account", "Password", "GroupId", "Contribution", "Gold", "Production", "CreateTime", "Resource", });
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
