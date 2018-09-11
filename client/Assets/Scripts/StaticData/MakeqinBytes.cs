// Generated by ProtoGen, Version=2.4.1.521, Culture=neutral, PublicKeyToken=null.  DO NOT EDIT!
#pragma warning disable
#region Designer generated code

using pb = global::Google.ProtocolBuffers;
using pbc = global::Google.ProtocolBuffers.Collections;
using pbd = global::Google.ProtocolBuffers.Descriptors;
using scg = global::System.Collections.Generic;
namespace com.nkm.framework.resource.data {
  
  public static partial class MakeqinBytes {
  
    #region Extension registration
    public static void RegisterAllExtensions(pb::ExtensionRegistry registry) {
    }
    #endregion
    #region Static variables
    #endregion
    #region Extensions
    internal static readonly object Descriptor;
    static MakeqinBytes() {
      Descriptor = null;
    }
    #endregion
    
  }
  #region Messages
  public sealed partial class MAKEQIN : pb::GeneratedMessageLite<MAKEQIN, MAKEQIN.Builder> {
    private MAKEQIN() { }
    private static readonly MAKEQIN defaultInstance = new MAKEQIN().MakeReadOnly();
    private static readonly string[] _mAKEQINFieldNames = new string[] { "id", "weapon_might" };
    private static readonly uint[] _mAKEQINFieldTags = new uint[] { 8, 16 };
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static MAKEQIN DefaultInstance {
      get { return defaultInstance; }
    }
    
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public override MAKEQIN DefaultInstanceForType {
      get { return DefaultInstance; }
    }
    
    protected override MAKEQIN ThisMessage {
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
    public const int WeaponMightFieldNumber = 2;
    private bool hasWeaponMight;
    private int weaponMight_;
    public bool HasWeaponMight {
      get { return hasWeaponMight; }
    }
    public int WeaponMight {
      get { return weaponMight_; }
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
      string[] field_names = _mAKEQINFieldNames;
      if (hasId) {
        output.WriteInt32(1, field_names[0], Id);
      }
      if (hasWeaponMight) {
        output.WriteInt32(2, field_names[1], WeaponMight);
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
        if (hasWeaponMight) {
          size += pb::CodedOutputStream.ComputeInt32Size(2, WeaponMight);
        }
        memoizedSerializedSize = size;
        return size;
      }
    }
    
    #region Lite runtime methods
    public override int GetHashCode() {
      int hash = GetType().GetHashCode();
      if (hasId) hash ^= id_.GetHashCode();
      if (hasWeaponMight) hash ^= weaponMight_.GetHashCode();
      return hash;
    }
    
    public override bool Equals(object obj) {
      MAKEQIN other = obj as MAKEQIN;
      if (other == null) return false;
      if (hasId != other.hasId || (hasId && !id_.Equals(other.id_))) return false;
      if (hasWeaponMight != other.hasWeaponMight || (hasWeaponMight && !weaponMight_.Equals(other.weaponMight_))) return false;
      return true;
    }
    
    #endregion
    
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static MAKEQIN ParseFrom(pb::ByteString data) {
      return ((Builder) CreateBuilder().MergeFrom(data)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static MAKEQIN ParseFrom(pb::ByteString data, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(data, extensionRegistry)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static MAKEQIN ParseFrom(byte[] data) {
      return ((Builder) CreateBuilder().MergeFrom(data)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static MAKEQIN ParseFrom(byte[] data, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(data, extensionRegistry)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static MAKEQIN ParseFrom(global::System.IO.Stream input) {
      return ((Builder) CreateBuilder().MergeFrom(input)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static MAKEQIN ParseFrom(global::System.IO.Stream input, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(input, extensionRegistry)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static MAKEQIN ParseDelimitedFrom(global::System.IO.Stream input) {
      return CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static MAKEQIN ParseDelimitedFrom(global::System.IO.Stream input, pb::ExtensionRegistry extensionRegistry) {
      return CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static MAKEQIN ParseFrom(pb::ICodedInputStream input) {
      return ((Builder) CreateBuilder().MergeFrom(input)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static MAKEQIN ParseFrom(pb::ICodedInputStream input, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(input, extensionRegistry)).BuildParsed();
    }
    private MAKEQIN MakeReadOnly() {
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
    public static Builder CreateBuilder(MAKEQIN prototype) {
      return new Builder(prototype);
    }
    
    public sealed partial class Builder : pb::GeneratedBuilderLite<MAKEQIN, Builder> {
      protected override Builder ThisBuilder {
        get { return this; }
      }
      public Builder() {
        result = DefaultInstance;
        resultIsReadOnly = true;
      }
      internal Builder(MAKEQIN cloneFrom) {
        result = cloneFrom;
        resultIsReadOnly = true;
      }
      
      private bool resultIsReadOnly;
      private MAKEQIN result;
      
      private MAKEQIN PrepareBuilder() {
        if (resultIsReadOnly) {
          MAKEQIN original = result;
          result = new MAKEQIN();
          resultIsReadOnly = false;
          MergeFrom(original);
        }
        return result;
      }
      
      public override bool IsInitialized {
        get { return result.IsInitialized; }
      }
      
      protected override MAKEQIN MessageBeingBuilt {
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
      
      public override MAKEQIN DefaultInstanceForType {
        get { return global::com.nkm.framework.resource.data.MAKEQIN.DefaultInstance; }
      }
      
      public override MAKEQIN BuildPartial() {
        if (resultIsReadOnly) {
          return result;
        }
        resultIsReadOnly = true;
        return result.MakeReadOnly();
      }
      
      public override Builder MergeFrom(pb::IMessageLite other) {
        if (other is MAKEQIN) {
          return MergeFrom((MAKEQIN) other);
        } else {
          base.MergeFrom(other);
          return this;
        }
      }
      
      public override Builder MergeFrom(MAKEQIN other) {
        if (other == global::com.nkm.framework.resource.data.MAKEQIN.DefaultInstance) return this;
        PrepareBuilder();
        if (other.HasId) {
          Id = other.Id;
        }
        if (other.HasWeaponMight) {
          WeaponMight = other.WeaponMight;
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
            int field_ordinal = global::System.Array.BinarySearch(_mAKEQINFieldNames, field_name, global::System.StringComparer.Ordinal);
            if(field_ordinal >= 0)
              tag = _mAKEQINFieldTags[field_ordinal];
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
              result.hasWeaponMight = input.ReadInt32(ref result.weaponMight_);
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
      
      public bool HasWeaponMight {
        get { return result.hasWeaponMight; }
      }
      public int WeaponMight {
        get { return result.WeaponMight; }
        set { SetWeaponMight(value); }
      }
      public Builder SetWeaponMight(int value) {
        PrepareBuilder();
        result.hasWeaponMight = true;
        result.weaponMight_ = value;
        return this;
      }
      public Builder ClearWeaponMight() {
        PrepareBuilder();
        result.hasWeaponMight = false;
        result.weaponMight_ = 0;
        return this;
      }
    }
    static MAKEQIN() {
      object.ReferenceEquals(global::com.nkm.framework.resource.data.MakeqinBytes.Descriptor, null);
    }
  }
  
  public sealed partial class MAKEQIN_ARRAY : pb::GeneratedMessageLite<MAKEQIN_ARRAY, MAKEQIN_ARRAY.Builder> {
    private MAKEQIN_ARRAY() { }
    private static readonly MAKEQIN_ARRAY defaultInstance = new MAKEQIN_ARRAY().MakeReadOnly();
    private static readonly string[] _mAKEQINARRAYFieldNames = new string[] { "items" };
    private static readonly uint[] _mAKEQINARRAYFieldTags = new uint[] { 10 };
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static MAKEQIN_ARRAY DefaultInstance {
      get { return defaultInstance; }
    }
    
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public override MAKEQIN_ARRAY DefaultInstanceForType {
      get { return DefaultInstance; }
    }
    
    protected override MAKEQIN_ARRAY ThisMessage {
      get { return this; }
    }
    
    #if UNITY_EDITOR
    [pb.FieldNumber]
    #endif//
    public const int ItemsFieldNumber = 1;
    private pbc::PopsicleList<global::com.nkm.framework.resource.data.MAKEQIN> items_ = new pbc::PopsicleList<global::com.nkm.framework.resource.data.MAKEQIN>();
    public scg::IList<global::com.nkm.framework.resource.data.MAKEQIN> ItemsList {
      get { return items_; }
    }
    public int ItemsCount {
      get { return items_.Count; }
    }
    public global::com.nkm.framework.resource.data.MAKEQIN GetItems(int index) {
      return items_[index];
    }
    
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public override bool IsInitialized {
      get {
        foreach (global::com.nkm.framework.resource.data.MAKEQIN element in ItemsList) {
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
      string[] field_names = _mAKEQINARRAYFieldNames;
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
        foreach (global::com.nkm.framework.resource.data.MAKEQIN element in ItemsList) {
          size += pb::CodedOutputStream.ComputeMessageSize(1, element);
        }
        memoizedSerializedSize = size;
        return size;
      }
    }
    
    #region Lite runtime methods
    public override int GetHashCode() {
      int hash = GetType().GetHashCode();
      foreach(global::com.nkm.framework.resource.data.MAKEQIN i in items_)
        hash ^= i.GetHashCode();
      return hash;
    }
    
    public override bool Equals(object obj) {
      MAKEQIN_ARRAY other = obj as MAKEQIN_ARRAY;
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
    public static MAKEQIN_ARRAY ParseFrom(pb::ByteString data) {
      return ((Builder) CreateBuilder().MergeFrom(data)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static MAKEQIN_ARRAY ParseFrom(pb::ByteString data, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(data, extensionRegistry)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static MAKEQIN_ARRAY ParseFrom(byte[] data) {
      return ((Builder) CreateBuilder().MergeFrom(data)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static MAKEQIN_ARRAY ParseFrom(byte[] data, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(data, extensionRegistry)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static MAKEQIN_ARRAY ParseFrom(global::System.IO.Stream input) {
      return ((Builder) CreateBuilder().MergeFrom(input)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static MAKEQIN_ARRAY ParseFrom(global::System.IO.Stream input, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(input, extensionRegistry)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static MAKEQIN_ARRAY ParseDelimitedFrom(global::System.IO.Stream input) {
      return CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static MAKEQIN_ARRAY ParseDelimitedFrom(global::System.IO.Stream input, pb::ExtensionRegistry extensionRegistry) {
      return CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static MAKEQIN_ARRAY ParseFrom(pb::ICodedInputStream input) {
      return ((Builder) CreateBuilder().MergeFrom(input)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static MAKEQIN_ARRAY ParseFrom(pb::ICodedInputStream input, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(input, extensionRegistry)).BuildParsed();
    }
    private MAKEQIN_ARRAY MakeReadOnly() {
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
    public static Builder CreateBuilder(MAKEQIN_ARRAY prototype) {
      return new Builder(prototype);
    }
    
    public sealed partial class Builder : pb::GeneratedBuilderLite<MAKEQIN_ARRAY, Builder> {
      protected override Builder ThisBuilder {
        get { return this; }
      }
      public Builder() {
        result = DefaultInstance;
        resultIsReadOnly = true;
      }
      internal Builder(MAKEQIN_ARRAY cloneFrom) {
        result = cloneFrom;
        resultIsReadOnly = true;
      }
      
      private bool resultIsReadOnly;
      private MAKEQIN_ARRAY result;
      
      private MAKEQIN_ARRAY PrepareBuilder() {
        if (resultIsReadOnly) {
          MAKEQIN_ARRAY original = result;
          result = new MAKEQIN_ARRAY();
          resultIsReadOnly = false;
          MergeFrom(original);
        }
        return result;
      }
      
      public override bool IsInitialized {
        get { return result.IsInitialized; }
      }
      
      protected override MAKEQIN_ARRAY MessageBeingBuilt {
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
      
      public override MAKEQIN_ARRAY DefaultInstanceForType {
        get { return global::com.nkm.framework.resource.data.MAKEQIN_ARRAY.DefaultInstance; }
      }
      
      public override MAKEQIN_ARRAY BuildPartial() {
        if (resultIsReadOnly) {
          return result;
        }
        resultIsReadOnly = true;
        return result.MakeReadOnly();
      }
      
      public override Builder MergeFrom(pb::IMessageLite other) {
        if (other is MAKEQIN_ARRAY) {
          return MergeFrom((MAKEQIN_ARRAY) other);
        } else {
          base.MergeFrom(other);
          return this;
        }
      }
      
      public override Builder MergeFrom(MAKEQIN_ARRAY other) {
        if (other == global::com.nkm.framework.resource.data.MAKEQIN_ARRAY.DefaultInstance) return this;
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
            int field_ordinal = global::System.Array.BinarySearch(_mAKEQINARRAYFieldNames, field_name, global::System.StringComparer.Ordinal);
            if(field_ordinal >= 0)
              tag = _mAKEQINARRAYFieldTags[field_ordinal];
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
              input.ReadMessageArray(tag, field_name, result.items_, global::com.nkm.framework.resource.data.MAKEQIN.DefaultInstance, extensionRegistry);
              break;
            }
          }
        }
        
        return this;
      }
      
      
      public pbc::IPopsicleList<global::com.nkm.framework.resource.data.MAKEQIN> ItemsList {
        get { return PrepareBuilder().items_; }
      }
      public int ItemsCount {
        get { return result.ItemsCount; }
      }
      public global::com.nkm.framework.resource.data.MAKEQIN GetItems(int index) {
        return result.GetItems(index);
      }
      public Builder SetItems(int index, global::com.nkm.framework.resource.data.MAKEQIN value) {
        pb::ThrowHelper.ThrowIfNull(value, "value");
        PrepareBuilder();
        result.items_[index] = value;
        return this;
      }
      public Builder SetItems(int index, global::com.nkm.framework.resource.data.MAKEQIN.Builder builderForValue) {
        pb::ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
        PrepareBuilder();
        result.items_[index] = builderForValue.Build();
        return this;
      }
      public Builder AddItems(global::com.nkm.framework.resource.data.MAKEQIN value) {
        pb::ThrowHelper.ThrowIfNull(value, "value");
        PrepareBuilder();
        result.items_.Add(value);
        return this;
      }
      public Builder AddItems(global::com.nkm.framework.resource.data.MAKEQIN.Builder builderForValue) {
        pb::ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
        PrepareBuilder();
        result.items_.Add(builderForValue.Build());
        return this;
      }
      public Builder AddRangeItems(scg::IEnumerable<global::com.nkm.framework.resource.data.MAKEQIN> values) {
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
    static MAKEQIN_ARRAY() {
      object.ReferenceEquals(global::com.nkm.framework.resource.data.MakeqinBytes.Descriptor, null);
    }
  }
  
  #endregion
  
}

#endregion Designer generated code
#pragma warning restore
