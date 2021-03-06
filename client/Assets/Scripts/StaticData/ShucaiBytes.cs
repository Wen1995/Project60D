// Generated by ProtoGen, Version=2.4.1.521, Culture=neutral, PublicKeyToken=null.  DO NOT EDIT!
#pragma warning disable
#region Designer generated code

using pb = global::Google.ProtocolBuffers;
using pbc = global::Google.ProtocolBuffers.Collections;
using pbd = global::Google.ProtocolBuffers.Descriptors;
using scg = global::System.Collections.Generic;
namespace com.nkm.framework.resource.data {
  
  public static partial class ShucaiBytes {
  
    #region Extension registration
    public static void RegisterAllExtensions(pb::ExtensionRegistry registry) {
    }
    #endregion
    #region Static variables
    #endregion
    #region Extensions
    internal static readonly object Descriptor;
    static ShucaiBytes() {
      Descriptor = null;
    }
    #endregion
    
  }
  #region Messages
  public sealed partial class SHUCAI : pb::GeneratedMessageLite<SHUCAI, SHUCAI.Builder> {
    private SHUCAI() { }
    private static readonly SHUCAI defaultInstance = new SHUCAI().MakeReadOnly();
    private static readonly string[] _sHUCAIFieldNames = new string[] { "id", "shucai_cap", "shucai_spd" };
    private static readonly uint[] _sHUCAIFieldTags = new uint[] { 8, 24, 16 };
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static SHUCAI DefaultInstance {
      get { return defaultInstance; }
    }
    
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public override SHUCAI DefaultInstanceForType {
      get { return DefaultInstance; }
    }
    
    protected override SHUCAI ThisMessage {
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
    public const int ShucaiSpdFieldNumber = 2;
    private bool hasShucaiSpd;
    private int shucaiSpd_;
    public bool HasShucaiSpd {
      get { return hasShucaiSpd; }
    }
    public int ShucaiSpd {
      get { return shucaiSpd_; }
    }
    
    #if UNITY_EDITOR
    [pb.FieldNumber]
    #endif//
    public const int ShucaiCapFieldNumber = 3;
    private bool hasShucaiCap;
    private int shucaiCap_;
    public bool HasShucaiCap {
      get { return hasShucaiCap; }
    }
    public int ShucaiCap {
      get { return shucaiCap_; }
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
      string[] field_names = _sHUCAIFieldNames;
      if (hasId) {
        output.WriteInt32(1, field_names[0], Id);
      }
      if (hasShucaiSpd) {
        output.WriteInt32(2, field_names[2], ShucaiSpd);
      }
      if (hasShucaiCap) {
        output.WriteInt32(3, field_names[1], ShucaiCap);
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
        if (hasShucaiSpd) {
          size += pb::CodedOutputStream.ComputeInt32Size(2, ShucaiSpd);
        }
        if (hasShucaiCap) {
          size += pb::CodedOutputStream.ComputeInt32Size(3, ShucaiCap);
        }
        memoizedSerializedSize = size;
        return size;
      }
    }
    
    #region Lite runtime methods
    public override int GetHashCode() {
      int hash = GetType().GetHashCode();
      if (hasId) hash ^= id_.GetHashCode();
      if (hasShucaiSpd) hash ^= shucaiSpd_.GetHashCode();
      if (hasShucaiCap) hash ^= shucaiCap_.GetHashCode();
      return hash;
    }
    
    public override bool Equals(object obj) {
      SHUCAI other = obj as SHUCAI;
      if (other == null) return false;
      if (hasId != other.hasId || (hasId && !id_.Equals(other.id_))) return false;
      if (hasShucaiSpd != other.hasShucaiSpd || (hasShucaiSpd && !shucaiSpd_.Equals(other.shucaiSpd_))) return false;
      if (hasShucaiCap != other.hasShucaiCap || (hasShucaiCap && !shucaiCap_.Equals(other.shucaiCap_))) return false;
      return true;
    }
    
    #endregion
    
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static SHUCAI ParseFrom(pb::ByteString data) {
      return ((Builder) CreateBuilder().MergeFrom(data)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static SHUCAI ParseFrom(pb::ByteString data, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(data, extensionRegistry)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static SHUCAI ParseFrom(byte[] data) {
      return ((Builder) CreateBuilder().MergeFrom(data)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static SHUCAI ParseFrom(byte[] data, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(data, extensionRegistry)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static SHUCAI ParseFrom(global::System.IO.Stream input) {
      return ((Builder) CreateBuilder().MergeFrom(input)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static SHUCAI ParseFrom(global::System.IO.Stream input, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(input, extensionRegistry)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static SHUCAI ParseDelimitedFrom(global::System.IO.Stream input) {
      return CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static SHUCAI ParseDelimitedFrom(global::System.IO.Stream input, pb::ExtensionRegistry extensionRegistry) {
      return CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static SHUCAI ParseFrom(pb::ICodedInputStream input) {
      return ((Builder) CreateBuilder().MergeFrom(input)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static SHUCAI ParseFrom(pb::ICodedInputStream input, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(input, extensionRegistry)).BuildParsed();
    }
    private SHUCAI MakeReadOnly() {
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
    public static Builder CreateBuilder(SHUCAI prototype) {
      return new Builder(prototype);
    }
    
    public sealed partial class Builder : pb::GeneratedBuilderLite<SHUCAI, Builder> {
      protected override Builder ThisBuilder {
        get { return this; }
      }
      public Builder() {
        result = DefaultInstance;
        resultIsReadOnly = true;
      }
      internal Builder(SHUCAI cloneFrom) {
        result = cloneFrom;
        resultIsReadOnly = true;
      }
      
      private bool resultIsReadOnly;
      private SHUCAI result;
      
      private SHUCAI PrepareBuilder() {
        if (resultIsReadOnly) {
          SHUCAI original = result;
          result = new SHUCAI();
          resultIsReadOnly = false;
          MergeFrom(original);
        }
        return result;
      }
      
      public override bool IsInitialized {
        get { return result.IsInitialized; }
      }
      
      protected override SHUCAI MessageBeingBuilt {
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
      
      public override SHUCAI DefaultInstanceForType {
        get { return global::com.nkm.framework.resource.data.SHUCAI.DefaultInstance; }
      }
      
      public override SHUCAI BuildPartial() {
        if (resultIsReadOnly) {
          return result;
        }
        resultIsReadOnly = true;
        return result.MakeReadOnly();
      }
      
      public override Builder MergeFrom(pb::IMessageLite other) {
        if (other is SHUCAI) {
          return MergeFrom((SHUCAI) other);
        } else {
          base.MergeFrom(other);
          return this;
        }
      }
      
      public override Builder MergeFrom(SHUCAI other) {
        if (other == global::com.nkm.framework.resource.data.SHUCAI.DefaultInstance) return this;
        PrepareBuilder();
        if (other.HasId) {
          Id = other.Id;
        }
        if (other.HasShucaiSpd) {
          ShucaiSpd = other.ShucaiSpd;
        }
        if (other.HasShucaiCap) {
          ShucaiCap = other.ShucaiCap;
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
            int field_ordinal = global::System.Array.BinarySearch(_sHUCAIFieldNames, field_name, global::System.StringComparer.Ordinal);
            if(field_ordinal >= 0)
              tag = _sHUCAIFieldTags[field_ordinal];
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
              result.hasShucaiSpd = input.ReadInt32(ref result.shucaiSpd_);
              break;
            }
            case 24: {
              result.hasShucaiCap = input.ReadInt32(ref result.shucaiCap_);
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
      
      public bool HasShucaiSpd {
        get { return result.hasShucaiSpd; }
      }
      public int ShucaiSpd {
        get { return result.ShucaiSpd; }
        set { SetShucaiSpd(value); }
      }
      public Builder SetShucaiSpd(int value) {
        PrepareBuilder();
        result.hasShucaiSpd = true;
        result.shucaiSpd_ = value;
        return this;
      }
      public Builder ClearShucaiSpd() {
        PrepareBuilder();
        result.hasShucaiSpd = false;
        result.shucaiSpd_ = 0;
        return this;
      }
      
      public bool HasShucaiCap {
        get { return result.hasShucaiCap; }
      }
      public int ShucaiCap {
        get { return result.ShucaiCap; }
        set { SetShucaiCap(value); }
      }
      public Builder SetShucaiCap(int value) {
        PrepareBuilder();
        result.hasShucaiCap = true;
        result.shucaiCap_ = value;
        return this;
      }
      public Builder ClearShucaiCap() {
        PrepareBuilder();
        result.hasShucaiCap = false;
        result.shucaiCap_ = 0;
        return this;
      }
    }
    static SHUCAI() {
      object.ReferenceEquals(global::com.nkm.framework.resource.data.ShucaiBytes.Descriptor, null);
    }
  }
  
  public sealed partial class SHUCAI_ARRAY : pb::GeneratedMessageLite<SHUCAI_ARRAY, SHUCAI_ARRAY.Builder> {
    private SHUCAI_ARRAY() { }
    private static readonly SHUCAI_ARRAY defaultInstance = new SHUCAI_ARRAY().MakeReadOnly();
    private static readonly string[] _sHUCAIARRAYFieldNames = new string[] { "items" };
    private static readonly uint[] _sHUCAIARRAYFieldTags = new uint[] { 10 };
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static SHUCAI_ARRAY DefaultInstance {
      get { return defaultInstance; }
    }
    
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public override SHUCAI_ARRAY DefaultInstanceForType {
      get { return DefaultInstance; }
    }
    
    protected override SHUCAI_ARRAY ThisMessage {
      get { return this; }
    }
    
    #if UNITY_EDITOR
    [pb.FieldNumber]
    #endif//
    public const int ItemsFieldNumber = 1;
    private pbc::PopsicleList<global::com.nkm.framework.resource.data.SHUCAI> items_ = new pbc::PopsicleList<global::com.nkm.framework.resource.data.SHUCAI>();
    public scg::IList<global::com.nkm.framework.resource.data.SHUCAI> ItemsList {
      get { return items_; }
    }
    public int ItemsCount {
      get { return items_.Count; }
    }
    public global::com.nkm.framework.resource.data.SHUCAI GetItems(int index) {
      return items_[index];
    }
    
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public override bool IsInitialized {
      get {
        foreach (global::com.nkm.framework.resource.data.SHUCAI element in ItemsList) {
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
      string[] field_names = _sHUCAIARRAYFieldNames;
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
        foreach (global::com.nkm.framework.resource.data.SHUCAI element in ItemsList) {
          size += pb::CodedOutputStream.ComputeMessageSize(1, element);
        }
        memoizedSerializedSize = size;
        return size;
      }
    }
    
    #region Lite runtime methods
    public override int GetHashCode() {
      int hash = GetType().GetHashCode();
      foreach(global::com.nkm.framework.resource.data.SHUCAI i in items_)
        hash ^= i.GetHashCode();
      return hash;
    }
    
    public override bool Equals(object obj) {
      SHUCAI_ARRAY other = obj as SHUCAI_ARRAY;
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
    public static SHUCAI_ARRAY ParseFrom(pb::ByteString data) {
      return ((Builder) CreateBuilder().MergeFrom(data)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static SHUCAI_ARRAY ParseFrom(pb::ByteString data, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(data, extensionRegistry)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static SHUCAI_ARRAY ParseFrom(byte[] data) {
      return ((Builder) CreateBuilder().MergeFrom(data)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static SHUCAI_ARRAY ParseFrom(byte[] data, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(data, extensionRegistry)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static SHUCAI_ARRAY ParseFrom(global::System.IO.Stream input) {
      return ((Builder) CreateBuilder().MergeFrom(input)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static SHUCAI_ARRAY ParseFrom(global::System.IO.Stream input, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(input, extensionRegistry)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static SHUCAI_ARRAY ParseDelimitedFrom(global::System.IO.Stream input) {
      return CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static SHUCAI_ARRAY ParseDelimitedFrom(global::System.IO.Stream input, pb::ExtensionRegistry extensionRegistry) {
      return CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static SHUCAI_ARRAY ParseFrom(pb::ICodedInputStream input) {
      return ((Builder) CreateBuilder().MergeFrom(input)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static SHUCAI_ARRAY ParseFrom(pb::ICodedInputStream input, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(input, extensionRegistry)).BuildParsed();
    }
    private SHUCAI_ARRAY MakeReadOnly() {
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
    public static Builder CreateBuilder(SHUCAI_ARRAY prototype) {
      return new Builder(prototype);
    }
    
    public sealed partial class Builder : pb::GeneratedBuilderLite<SHUCAI_ARRAY, Builder> {
      protected override Builder ThisBuilder {
        get { return this; }
      }
      public Builder() {
        result = DefaultInstance;
        resultIsReadOnly = true;
      }
      internal Builder(SHUCAI_ARRAY cloneFrom) {
        result = cloneFrom;
        resultIsReadOnly = true;
      }
      
      private bool resultIsReadOnly;
      private SHUCAI_ARRAY result;
      
      private SHUCAI_ARRAY PrepareBuilder() {
        if (resultIsReadOnly) {
          SHUCAI_ARRAY original = result;
          result = new SHUCAI_ARRAY();
          resultIsReadOnly = false;
          MergeFrom(original);
        }
        return result;
      }
      
      public override bool IsInitialized {
        get { return result.IsInitialized; }
      }
      
      protected override SHUCAI_ARRAY MessageBeingBuilt {
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
      
      public override SHUCAI_ARRAY DefaultInstanceForType {
        get { return global::com.nkm.framework.resource.data.SHUCAI_ARRAY.DefaultInstance; }
      }
      
      public override SHUCAI_ARRAY BuildPartial() {
        if (resultIsReadOnly) {
          return result;
        }
        resultIsReadOnly = true;
        return result.MakeReadOnly();
      }
      
      public override Builder MergeFrom(pb::IMessageLite other) {
        if (other is SHUCAI_ARRAY) {
          return MergeFrom((SHUCAI_ARRAY) other);
        } else {
          base.MergeFrom(other);
          return this;
        }
      }
      
      public override Builder MergeFrom(SHUCAI_ARRAY other) {
        if (other == global::com.nkm.framework.resource.data.SHUCAI_ARRAY.DefaultInstance) return this;
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
            int field_ordinal = global::System.Array.BinarySearch(_sHUCAIARRAYFieldNames, field_name, global::System.StringComparer.Ordinal);
            if(field_ordinal >= 0)
              tag = _sHUCAIARRAYFieldTags[field_ordinal];
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
              input.ReadMessageArray(tag, field_name, result.items_, global::com.nkm.framework.resource.data.SHUCAI.DefaultInstance, extensionRegistry);
              break;
            }
          }
        }
        
        return this;
      }
      
      
      public pbc::IPopsicleList<global::com.nkm.framework.resource.data.SHUCAI> ItemsList {
        get { return PrepareBuilder().items_; }
      }
      public int ItemsCount {
        get { return result.ItemsCount; }
      }
      public global::com.nkm.framework.resource.data.SHUCAI GetItems(int index) {
        return result.GetItems(index);
      }
      public Builder SetItems(int index, global::com.nkm.framework.resource.data.SHUCAI value) {
        pb::ThrowHelper.ThrowIfNull(value, "value");
        PrepareBuilder();
        result.items_[index] = value;
        return this;
      }
      public Builder SetItems(int index, global::com.nkm.framework.resource.data.SHUCAI.Builder builderForValue) {
        pb::ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
        PrepareBuilder();
        result.items_[index] = builderForValue.Build();
        return this;
      }
      public Builder AddItems(global::com.nkm.framework.resource.data.SHUCAI value) {
        pb::ThrowHelper.ThrowIfNull(value, "value");
        PrepareBuilder();
        result.items_.Add(value);
        return this;
      }
      public Builder AddItems(global::com.nkm.framework.resource.data.SHUCAI.Builder builderForValue) {
        pb::ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
        PrepareBuilder();
        result.items_.Add(builderForValue.Build());
        return this;
      }
      public Builder AddRangeItems(scg::IEnumerable<global::com.nkm.framework.resource.data.SHUCAI> values) {
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
    static SHUCAI_ARRAY() {
      object.ReferenceEquals(global::com.nkm.framework.resource.data.ShucaiBytes.Descriptor, null);
    }
  }
  
  #endregion
  
}

#endregion Designer generated code
#pragma warning restore
