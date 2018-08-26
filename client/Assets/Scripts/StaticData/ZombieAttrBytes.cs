// Generated by ProtoGen, Version=2.4.1.521, Culture=neutral, PublicKeyToken=null.  DO NOT EDIT!
#pragma warning disable
#region Designer generated code

using pb = global::Google.ProtocolBuffers;
using pbc = global::Google.ProtocolBuffers.Collections;
using pbd = global::Google.ProtocolBuffers.Descriptors;
using scg = global::System.Collections.Generic;
namespace com.game.framework.resource.data {
  
  public static partial class ZombieAttrBytes {
  
    #region Extension registration
    public static void RegisterAllExtensions(pb::ExtensionRegistry registry) {
    }
    #endregion
    #region Static variables
    #endregion
    #region Extensions
    internal static readonly object Descriptor;
    static ZombieAttrBytes() {
      Descriptor = null;
    }
    #endregion
    
  }
  #region Messages
  public sealed partial class ZOMBIE_ATTR : pb::GeneratedMessageLite<ZOMBIE_ATTR, ZOMBIE_ATTR.Builder> {
    private ZOMBIE_ATTR() { }
    private static readonly ZOMBIE_ATTR defaultInstance = new ZOMBIE_ATTR().MakeReadOnly();
    private static readonly string[] _zOMBIEATTRFieldNames = new string[] { "id", "manorcap_zombie", "zombie_atk", "zombie_def", "zombie_hp", "zombie_num" };
    private static readonly uint[] _zOMBIEATTRFieldTags = new uint[] { 8, 16, 24, 32, 40, 48 };
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static ZOMBIE_ATTR DefaultInstance {
      get { return defaultInstance; }
    }
    
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public override ZOMBIE_ATTR DefaultInstanceForType {
      get { return DefaultInstance; }
    }
    
    protected override ZOMBIE_ATTR ThisMessage {
      get { return this; }
    }
    
    #if UNITY_EDITOR
    [pb.FieldNumber]
    #endif//
    public const int IdFieldNumber = 1;
    private bool hasId;
    private int id_;
    public bool HasId {
      get { return hasId; }
    }
    public int Id {
      get { return id_; }
    }
    
    #if UNITY_EDITOR
    [pb.FieldNumber]
    #endif//
    public const int ManorcapZombieFieldNumber = 2;
    private bool hasManorcapZombie;
    private int manorcapZombie_;
    public bool HasManorcapZombie {
      get { return hasManorcapZombie; }
    }
    public int ManorcapZombie {
      get { return manorcapZombie_; }
    }
    
    #if UNITY_EDITOR
    [pb.FieldNumber]
    #endif//
    public const int ZombieAtkFieldNumber = 3;
    private bool hasZombieAtk;
    private int zombieAtk_;
    public bool HasZombieAtk {
      get { return hasZombieAtk; }
    }
    public int ZombieAtk {
      get { return zombieAtk_; }
    }
    
    #if UNITY_EDITOR
    [pb.FieldNumber]
    #endif//
    public const int ZombieDefFieldNumber = 4;
    private bool hasZombieDef;
    private int zombieDef_;
    public bool HasZombieDef {
      get { return hasZombieDef; }
    }
    public int ZombieDef {
      get { return zombieDef_; }
    }
    
    #if UNITY_EDITOR
    [pb.FieldNumber]
    #endif//
    public const int ZombieHpFieldNumber = 5;
    private bool hasZombieHp;
    private int zombieHp_;
    public bool HasZombieHp {
      get { return hasZombieHp; }
    }
    public int ZombieHp {
      get { return zombieHp_; }
    }
    
    #if UNITY_EDITOR
    [pb.FieldNumber]
    #endif//
    public const int ZombieNumFieldNumber = 6;
    private bool hasZombieNum;
    private int zombieNum_;
    public bool HasZombieNum {
      get { return hasZombieNum; }
    }
    public int ZombieNum {
      get { return zombieNum_; }
    }
    
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public override bool IsInitialized {
      get {
        if (!hasId) return false;
        return true;
      }
    }
    
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public override void WriteTo(pb::ICodedOutputStream output) {
      int size = SerializedSize;
      string[] field_names = _zOMBIEATTRFieldNames;
      if (hasId) {
        output.WriteInt32(1, field_names[0], Id);
      }
      if (hasManorcapZombie) {
        output.WriteInt32(2, field_names[1], ManorcapZombie);
      }
      if (hasZombieAtk) {
        output.WriteInt32(3, field_names[2], ZombieAtk);
      }
      if (hasZombieDef) {
        output.WriteInt32(4, field_names[3], ZombieDef);
      }
      if (hasZombieHp) {
        output.WriteInt32(5, field_names[4], ZombieHp);
      }
      if (hasZombieNum) {
        output.WriteInt32(6, field_names[5], ZombieNum);
      }
    }
    
    private int memoizedSerializedSize = -1;
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public override int SerializedSize {
      get {
        int size = memoizedSerializedSize;
        if (size != -1) return size;
        
        size = 0;
        if (hasId) {
          size += pb::CodedOutputStream.ComputeInt32Size(1, Id);
        }
        if (hasManorcapZombie) {
          size += pb::CodedOutputStream.ComputeInt32Size(2, ManorcapZombie);
        }
        if (hasZombieAtk) {
          size += pb::CodedOutputStream.ComputeInt32Size(3, ZombieAtk);
        }
        if (hasZombieDef) {
          size += pb::CodedOutputStream.ComputeInt32Size(4, ZombieDef);
        }
        if (hasZombieHp) {
          size += pb::CodedOutputStream.ComputeInt32Size(5, ZombieHp);
        }
        if (hasZombieNum) {
          size += pb::CodedOutputStream.ComputeInt32Size(6, ZombieNum);
        }
        memoizedSerializedSize = size;
        return size;
      }
    }
    
    #region Lite runtime methods
    public override int GetHashCode() {
      int hash = GetType().GetHashCode();
      if (hasId) hash ^= id_.GetHashCode();
      if (hasManorcapZombie) hash ^= manorcapZombie_.GetHashCode();
      if (hasZombieAtk) hash ^= zombieAtk_.GetHashCode();
      if (hasZombieDef) hash ^= zombieDef_.GetHashCode();
      if (hasZombieHp) hash ^= zombieHp_.GetHashCode();
      if (hasZombieNum) hash ^= zombieNum_.GetHashCode();
      return hash;
    }
    
    public override bool Equals(object obj) {
      ZOMBIE_ATTR other = obj as ZOMBIE_ATTR;
      if (other == null) return false;
      if (hasId != other.hasId || (hasId && !id_.Equals(other.id_))) return false;
      if (hasManorcapZombie != other.hasManorcapZombie || (hasManorcapZombie && !manorcapZombie_.Equals(other.manorcapZombie_))) return false;
      if (hasZombieAtk != other.hasZombieAtk || (hasZombieAtk && !zombieAtk_.Equals(other.zombieAtk_))) return false;
      if (hasZombieDef != other.hasZombieDef || (hasZombieDef && !zombieDef_.Equals(other.zombieDef_))) return false;
      if (hasZombieHp != other.hasZombieHp || (hasZombieHp && !zombieHp_.Equals(other.zombieHp_))) return false;
      if (hasZombieNum != other.hasZombieNum || (hasZombieNum && !zombieNum_.Equals(other.zombieNum_))) return false;
      return true;
    }
    
    #endregion
    
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static ZOMBIE_ATTR ParseFrom(pb::ByteString data) {
      return ((Builder) CreateBuilder().MergeFrom(data)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static ZOMBIE_ATTR ParseFrom(pb::ByteString data, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(data, extensionRegistry)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static ZOMBIE_ATTR ParseFrom(byte[] data) {
      return ((Builder) CreateBuilder().MergeFrom(data)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static ZOMBIE_ATTR ParseFrom(byte[] data, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(data, extensionRegistry)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static ZOMBIE_ATTR ParseFrom(global::System.IO.Stream input) {
      return ((Builder) CreateBuilder().MergeFrom(input)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static ZOMBIE_ATTR ParseFrom(global::System.IO.Stream input, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(input, extensionRegistry)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static ZOMBIE_ATTR ParseDelimitedFrom(global::System.IO.Stream input) {
      return CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static ZOMBIE_ATTR ParseDelimitedFrom(global::System.IO.Stream input, pb::ExtensionRegistry extensionRegistry) {
      return CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static ZOMBIE_ATTR ParseFrom(pb::ICodedInputStream input) {
      return ((Builder) CreateBuilder().MergeFrom(input)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static ZOMBIE_ATTR ParseFrom(pb::ICodedInputStream input, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(input, extensionRegistry)).BuildParsed();
    }
    private ZOMBIE_ATTR MakeReadOnly() {
      return this;
    }
    
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static Builder CreateBuilder() { return new Builder(); }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public override Builder ToBuilder() { return CreateBuilder(this); }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public override Builder CreateBuilderForType() { return new Builder(); }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static Builder CreateBuilder(ZOMBIE_ATTR prototype) {
      return new Builder(prototype);
    }
    
    public sealed partial class Builder : pb::GeneratedBuilderLite<ZOMBIE_ATTR, Builder> {
      protected override Builder ThisBuilder {
        get { return this; }
      }
      public Builder() {
        result = DefaultInstance;
        resultIsReadOnly = true;
      }
      internal Builder(ZOMBIE_ATTR cloneFrom) {
        result = cloneFrom;
        resultIsReadOnly = true;
      }
      
      private bool resultIsReadOnly;
      private ZOMBIE_ATTR result;
      
      private ZOMBIE_ATTR PrepareBuilder() {
        if (resultIsReadOnly) {
          ZOMBIE_ATTR original = result;
          result = new ZOMBIE_ATTR();
          resultIsReadOnly = false;
          MergeFrom(original);
        }
        return result;
      }
      
      public override bool IsInitialized {
        get { return result.IsInitialized; }
      }
      
      protected override ZOMBIE_ATTR MessageBeingBuilt {
        get { return PrepareBuilder(); }
      }
      
      public override Builder Clear() {
        result = DefaultInstance;
        resultIsReadOnly = true;
        return this;
      }
      
      public override Builder Clone() {
        if (resultIsReadOnly) {
          return new Builder(result);
        } else {
          return new Builder().MergeFrom(result);
        }
      }
      
      public override ZOMBIE_ATTR DefaultInstanceForType {
        get { return global::com.game.framework.resource.data.ZOMBIE_ATTR.DefaultInstance; }
      }
      
      public override ZOMBIE_ATTR BuildPartial() {
        if (resultIsReadOnly) {
          return result;
        }
        resultIsReadOnly = true;
        return result.MakeReadOnly();
      }
      
      public override Builder MergeFrom(pb::IMessageLite other) {
        if (other is ZOMBIE_ATTR) {
          return MergeFrom((ZOMBIE_ATTR) other);
        } else {
          base.MergeFrom(other);
          return this;
        }
      }
      
      public override Builder MergeFrom(ZOMBIE_ATTR other) {
        if (other == global::com.game.framework.resource.data.ZOMBIE_ATTR.DefaultInstance) return this;
        PrepareBuilder();
        if (other.HasId) {
          Id = other.Id;
        }
        if (other.HasManorcapZombie) {
          ManorcapZombie = other.ManorcapZombie;
        }
        if (other.HasZombieAtk) {
          ZombieAtk = other.ZombieAtk;
        }
        if (other.HasZombieDef) {
          ZombieDef = other.ZombieDef;
        }
        if (other.HasZombieHp) {
          ZombieHp = other.ZombieHp;
        }
        if (other.HasZombieNum) {
          ZombieNum = other.ZombieNum;
        }
        return this;
      }
      
      public override Builder MergeFrom(pb::ICodedInputStream input) {
        return MergeFrom(input, pb::ExtensionRegistry.Empty);
      }
      
      public override Builder MergeFrom(pb::ICodedInputStream input, pb::ExtensionRegistry extensionRegistry) {
        PrepareBuilder();
        uint tag;
        string field_name;
        while (input.ReadTag(out tag, out field_name)) {
          if(tag == 0 && field_name != null) {
            int field_ordinal = global::System.Array.BinarySearch(_zOMBIEATTRFieldNames, field_name, global::System.StringComparer.Ordinal);
            if(field_ordinal >= 0)
              tag = _zOMBIEATTRFieldTags[field_ordinal];
            else {
              ParseUnknownField(input, extensionRegistry, tag, field_name);
              continue;
            }
          }
          switch (tag) {
            case 0: {
              throw pb::InvalidProtocolBufferException.InvalidTag();
            }
            default: {
              if (pb::WireFormat.IsEndGroupTag(tag)) {
                return this;
              }
              ParseUnknownField(input, extensionRegistry, tag, field_name);
              break;
            }
            case 8: {
              result.hasId = input.ReadInt32(ref result.id_);
              break;
            }
            case 16: {
              result.hasManorcapZombie = input.ReadInt32(ref result.manorcapZombie_);
              break;
            }
            case 24: {
              result.hasZombieAtk = input.ReadInt32(ref result.zombieAtk_);
              break;
            }
            case 32: {
              result.hasZombieDef = input.ReadInt32(ref result.zombieDef_);
              break;
            }
            case 40: {
              result.hasZombieHp = input.ReadInt32(ref result.zombieHp_);
              break;
            }
            case 48: {
              result.hasZombieNum = input.ReadInt32(ref result.zombieNum_);
              break;
            }
          }
        }
        
        return this;
      }
      
      
      public bool HasId {
        get { return result.hasId; }
      }
      public int Id {
        get { return result.Id; }
        set { SetId(value); }
      }
      public Builder SetId(int value) {
        PrepareBuilder();
        result.hasId = true;
        result.id_ = value;
        return this;
      }
      public Builder ClearId() {
        PrepareBuilder();
        result.hasId = false;
        result.id_ = 0;
        return this;
      }
      
      public bool HasManorcapZombie {
        get { return result.hasManorcapZombie; }
      }
      public int ManorcapZombie {
        get { return result.ManorcapZombie; }
        set { SetManorcapZombie(value); }
      }
      public Builder SetManorcapZombie(int value) {
        PrepareBuilder();
        result.hasManorcapZombie = true;
        result.manorcapZombie_ = value;
        return this;
      }
      public Builder ClearManorcapZombie() {
        PrepareBuilder();
        result.hasManorcapZombie = false;
        result.manorcapZombie_ = 0;
        return this;
      }
      
      public bool HasZombieAtk {
        get { return result.hasZombieAtk; }
      }
      public int ZombieAtk {
        get { return result.ZombieAtk; }
        set { SetZombieAtk(value); }
      }
      public Builder SetZombieAtk(int value) {
        PrepareBuilder();
        result.hasZombieAtk = true;
        result.zombieAtk_ = value;
        return this;
      }
      public Builder ClearZombieAtk() {
        PrepareBuilder();
        result.hasZombieAtk = false;
        result.zombieAtk_ = 0;
        return this;
      }
      
      public bool HasZombieDef {
        get { return result.hasZombieDef; }
      }
      public int ZombieDef {
        get { return result.ZombieDef; }
        set { SetZombieDef(value); }
      }
      public Builder SetZombieDef(int value) {
        PrepareBuilder();
        result.hasZombieDef = true;
        result.zombieDef_ = value;
        return this;
      }
      public Builder ClearZombieDef() {
        PrepareBuilder();
        result.hasZombieDef = false;
        result.zombieDef_ = 0;
        return this;
      }
      
      public bool HasZombieHp {
        get { return result.hasZombieHp; }
      }
      public int ZombieHp {
        get { return result.ZombieHp; }
        set { SetZombieHp(value); }
      }
      public Builder SetZombieHp(int value) {
        PrepareBuilder();
        result.hasZombieHp = true;
        result.zombieHp_ = value;
        return this;
      }
      public Builder ClearZombieHp() {
        PrepareBuilder();
        result.hasZombieHp = false;
        result.zombieHp_ = 0;
        return this;
      }
      
      public bool HasZombieNum {
        get { return result.hasZombieNum; }
      }
      public int ZombieNum {
        get { return result.ZombieNum; }
        set { SetZombieNum(value); }
      }
      public Builder SetZombieNum(int value) {
        PrepareBuilder();
        result.hasZombieNum = true;
        result.zombieNum_ = value;
        return this;
      }
      public Builder ClearZombieNum() {
        PrepareBuilder();
        result.hasZombieNum = false;
        result.zombieNum_ = 0;
        return this;
      }
    }
    static ZOMBIE_ATTR() {
      object.ReferenceEquals(global::com.game.framework.resource.data.ZombieAttrBytes.Descriptor, null);
    }
  }
  
  public sealed partial class ZOMBIE_ATTR_ARRAY : pb::GeneratedMessageLite<ZOMBIE_ATTR_ARRAY, ZOMBIE_ATTR_ARRAY.Builder> {
    private ZOMBIE_ATTR_ARRAY() { }
    private static readonly ZOMBIE_ATTR_ARRAY defaultInstance = new ZOMBIE_ATTR_ARRAY().MakeReadOnly();
    private static readonly string[] _zOMBIEATTRARRAYFieldNames = new string[] { "items" };
    private static readonly uint[] _zOMBIEATTRARRAYFieldTags = new uint[] { 10 };
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static ZOMBIE_ATTR_ARRAY DefaultInstance {
      get { return defaultInstance; }
    }
    
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public override ZOMBIE_ATTR_ARRAY DefaultInstanceForType {
      get { return DefaultInstance; }
    }
    
    protected override ZOMBIE_ATTR_ARRAY ThisMessage {
      get { return this; }
    }
    
    #if UNITY_EDITOR
    [pb.FieldNumber]
    #endif//
    public const int ItemsFieldNumber = 1;
    private pbc::PopsicleList<global::com.game.framework.resource.data.ZOMBIE_ATTR> items_ = new pbc::PopsicleList<global::com.game.framework.resource.data.ZOMBIE_ATTR>();
    public scg::IList<global::com.game.framework.resource.data.ZOMBIE_ATTR> ItemsList {
      get { return items_; }
    }
    public int ItemsCount {
      get { return items_.Count; }
    }
    public global::com.game.framework.resource.data.ZOMBIE_ATTR GetItems(int index) {
      return items_[index];
    }
    
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public override bool IsInitialized {
      get {
        foreach (global::com.game.framework.resource.data.ZOMBIE_ATTR element in ItemsList) {
          if (!element.IsInitialized) return false;
        }
        return true;
      }
    }
    
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public override void WriteTo(pb::ICodedOutputStream output) {
      int size = SerializedSize;
      string[] field_names = _zOMBIEATTRARRAYFieldNames;
      if (items_.Count > 0) {
        output.WriteMessageArray(1, field_names[0], items_);
      }
    }
    
    private int memoizedSerializedSize = -1;
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public override int SerializedSize {
      get {
        int size = memoizedSerializedSize;
        if (size != -1) return size;
        
        size = 0;
        foreach (global::com.game.framework.resource.data.ZOMBIE_ATTR element in ItemsList) {
          size += pb::CodedOutputStream.ComputeMessageSize(1, element);
        }
        memoizedSerializedSize = size;
        return size;
      }
    }
    
    #region Lite runtime methods
    public override int GetHashCode() {
      int hash = GetType().GetHashCode();
      foreach(global::com.game.framework.resource.data.ZOMBIE_ATTR i in items_)
        hash ^= i.GetHashCode();
      return hash;
    }
    
    public override bool Equals(object obj) {
      ZOMBIE_ATTR_ARRAY other = obj as ZOMBIE_ATTR_ARRAY;
      if (other == null) return false;
      if(items_.Count != other.items_.Count) return false;
      for(int ix=0; ix < items_.Count; ix++)
        if(!items_[ix].Equals(other.items_[ix])) return false;
      return true;
    }
    
    #endregion
    
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static ZOMBIE_ATTR_ARRAY ParseFrom(pb::ByteString data) {
      return ((Builder) CreateBuilder().MergeFrom(data)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static ZOMBIE_ATTR_ARRAY ParseFrom(pb::ByteString data, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(data, extensionRegistry)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static ZOMBIE_ATTR_ARRAY ParseFrom(byte[] data) {
      return ((Builder) CreateBuilder().MergeFrom(data)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static ZOMBIE_ATTR_ARRAY ParseFrom(byte[] data, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(data, extensionRegistry)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static ZOMBIE_ATTR_ARRAY ParseFrom(global::System.IO.Stream input) {
      return ((Builder) CreateBuilder().MergeFrom(input)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static ZOMBIE_ATTR_ARRAY ParseFrom(global::System.IO.Stream input, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(input, extensionRegistry)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static ZOMBIE_ATTR_ARRAY ParseDelimitedFrom(global::System.IO.Stream input) {
      return CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static ZOMBIE_ATTR_ARRAY ParseDelimitedFrom(global::System.IO.Stream input, pb::ExtensionRegistry extensionRegistry) {
      return CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static ZOMBIE_ATTR_ARRAY ParseFrom(pb::ICodedInputStream input) {
      return ((Builder) CreateBuilder().MergeFrom(input)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static ZOMBIE_ATTR_ARRAY ParseFrom(pb::ICodedInputStream input, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(input, extensionRegistry)).BuildParsed();
    }
    private ZOMBIE_ATTR_ARRAY MakeReadOnly() {
      items_.MakeReadOnly();
      return this;
    }
    
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static Builder CreateBuilder() { return new Builder(); }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public override Builder ToBuilder() { return CreateBuilder(this); }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public override Builder CreateBuilderForType() { return new Builder(); }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static Builder CreateBuilder(ZOMBIE_ATTR_ARRAY prototype) {
      return new Builder(prototype);
    }
    
    public sealed partial class Builder : pb::GeneratedBuilderLite<ZOMBIE_ATTR_ARRAY, Builder> {
      protected override Builder ThisBuilder {
        get { return this; }
      }
      public Builder() {
        result = DefaultInstance;
        resultIsReadOnly = true;
      }
      internal Builder(ZOMBIE_ATTR_ARRAY cloneFrom) {
        result = cloneFrom;
        resultIsReadOnly = true;
      }
      
      private bool resultIsReadOnly;
      private ZOMBIE_ATTR_ARRAY result;
      
      private ZOMBIE_ATTR_ARRAY PrepareBuilder() {
        if (resultIsReadOnly) {
          ZOMBIE_ATTR_ARRAY original = result;
          result = new ZOMBIE_ATTR_ARRAY();
          resultIsReadOnly = false;
          MergeFrom(original);
        }
        return result;
      }
      
      public override bool IsInitialized {
        get { return result.IsInitialized; }
      }
      
      protected override ZOMBIE_ATTR_ARRAY MessageBeingBuilt {
        get { return PrepareBuilder(); }
      }
      
      public override Builder Clear() {
        result = DefaultInstance;
        resultIsReadOnly = true;
        return this;
      }
      
      public override Builder Clone() {
        if (resultIsReadOnly) {
          return new Builder(result);
        } else {
          return new Builder().MergeFrom(result);
        }
      }
      
      public override ZOMBIE_ATTR_ARRAY DefaultInstanceForType {
        get { return global::com.game.framework.resource.data.ZOMBIE_ATTR_ARRAY.DefaultInstance; }
      }
      
      public override ZOMBIE_ATTR_ARRAY BuildPartial() {
        if (resultIsReadOnly) {
          return result;
        }
        resultIsReadOnly = true;
        return result.MakeReadOnly();
      }
      
      public override Builder MergeFrom(pb::IMessageLite other) {
        if (other is ZOMBIE_ATTR_ARRAY) {
          return MergeFrom((ZOMBIE_ATTR_ARRAY) other);
        } else {
          base.MergeFrom(other);
          return this;
        }
      }
      
      public override Builder MergeFrom(ZOMBIE_ATTR_ARRAY other) {
        if (other == global::com.game.framework.resource.data.ZOMBIE_ATTR_ARRAY.DefaultInstance) return this;
        PrepareBuilder();
        if (other.items_.Count != 0) {
          result.items_.Add(other.items_);
        }
        return this;
      }
      
      public override Builder MergeFrom(pb::ICodedInputStream input) {
        return MergeFrom(input, pb::ExtensionRegistry.Empty);
      }
      
      public override Builder MergeFrom(pb::ICodedInputStream input, pb::ExtensionRegistry extensionRegistry) {
        PrepareBuilder();
        uint tag;
        string field_name;
        while (input.ReadTag(out tag, out field_name)) {
          if(tag == 0 && field_name != null) {
            int field_ordinal = global::System.Array.BinarySearch(_zOMBIEATTRARRAYFieldNames, field_name, global::System.StringComparer.Ordinal);
            if(field_ordinal >= 0)
              tag = _zOMBIEATTRARRAYFieldTags[field_ordinal];
            else {
              ParseUnknownField(input, extensionRegistry, tag, field_name);
              continue;
            }
          }
          switch (tag) {
            case 0: {
              throw pb::InvalidProtocolBufferException.InvalidTag();
            }
            default: {
              if (pb::WireFormat.IsEndGroupTag(tag)) {
                return this;
              }
              ParseUnknownField(input, extensionRegistry, tag, field_name);
              break;
            }
            case 10: {
              input.ReadMessageArray(tag, field_name, result.items_, global::com.game.framework.resource.data.ZOMBIE_ATTR.DefaultInstance, extensionRegistry);
              break;
            }
          }
        }
        
        return this;
      }
      
      
      public pbc::IPopsicleList<global::com.game.framework.resource.data.ZOMBIE_ATTR> ItemsList {
        get { return PrepareBuilder().items_; }
      }
      public int ItemsCount {
        get { return result.ItemsCount; }
      }
      public global::com.game.framework.resource.data.ZOMBIE_ATTR GetItems(int index) {
        return result.GetItems(index);
      }
      public Builder SetItems(int index, global::com.game.framework.resource.data.ZOMBIE_ATTR value) {
        pb::ThrowHelper.ThrowIfNull(value, "value");
        PrepareBuilder();
        result.items_[index] = value;
        return this;
      }
      public Builder SetItems(int index, global::com.game.framework.resource.data.ZOMBIE_ATTR.Builder builderForValue) {
        pb::ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
        PrepareBuilder();
        result.items_[index] = builderForValue.Build();
        return this;
      }
      public Builder AddItems(global::com.game.framework.resource.data.ZOMBIE_ATTR value) {
        pb::ThrowHelper.ThrowIfNull(value, "value");
        PrepareBuilder();
        result.items_.Add(value);
        return this;
      }
      public Builder AddItems(global::com.game.framework.resource.data.ZOMBIE_ATTR.Builder builderForValue) {
        pb::ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
        PrepareBuilder();
        result.items_.Add(builderForValue.Build());
        return this;
      }
      public Builder AddRangeItems(scg::IEnumerable<global::com.game.framework.resource.data.ZOMBIE_ATTR> values) {
        PrepareBuilder();
        result.items_.Add(values);
        return this;
      }
      public Builder ClearItems() {
        PrepareBuilder();
        result.items_.Clear();
        return this;
      }
    }
    static ZOMBIE_ATTR_ARRAY() {
      object.ReferenceEquals(global::com.game.framework.resource.data.ZombieAttrBytes.Descriptor, null);
    }
  }
  
  #endregion
  
}

#endregion Designer generated code
#pragma warning restore