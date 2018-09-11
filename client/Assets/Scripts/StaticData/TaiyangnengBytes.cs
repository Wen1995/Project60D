// Generated by ProtoGen, Version=2.4.1.521, Culture=neutral, PublicKeyToken=null.  DO NOT EDIT!
#pragma warning disable
#region Designer generated code

using pb = global::Google.ProtocolBuffers;
using pbc = global::Google.ProtocolBuffers.Collections;
using pbd = global::Google.ProtocolBuffers.Descriptors;
using scg = global::System.Collections.Generic;
namespace com.nkm.framework.resource.data {
  
  public static partial class TaiyangnengBytes {
  
    #region Extension registration
    public static void RegisterAllExtensions(pb::ExtensionRegistry registry) {
    }
    #endregion
    #region Static variables
    #endregion
    #region Extensions
    internal static readonly object Descriptor;
    static TaiyangnengBytes() {
      Descriptor = null;
    }
    #endregion
    
  }
  #region Messages
  public sealed partial class TAIYANGNENG : pb::GeneratedMessageLite<TAIYANGNENG, TAIYANGNENG.Builder> {
    private TAIYANGNENG() { }
    private static readonly TAIYANGNENG defaultInstance = new TAIYANGNENG().MakeReadOnly();
    private static readonly string[] _tAIYANGNENGFieldNames = new string[] { "id", "taiyangneng_cap", "taiyangneng_spd" };
    private static readonly uint[] _tAIYANGNENGFieldTags = new uint[] { 8, 24, 16 };
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static TAIYANGNENG DefaultInstance {
      get { return defaultInstance; }
    }
    
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public override TAIYANGNENG DefaultInstanceForType {
      get { return DefaultInstance; }
    }
    
    protected override TAIYANGNENG ThisMessage {
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
    public const int TaiyangnengSpdFieldNumber = 2;
    private bool hasTaiyangnengSpd;
    private int taiyangnengSpd_;
    public bool HasTaiyangnengSpd {
      get { return hasTaiyangnengSpd; }
    }
    public int TaiyangnengSpd {
      get { return taiyangnengSpd_; }
    }
    
    #if UNITY_EDITOR
    [pb.FieldNumber]
    #endif//
    public const int TaiyangnengCapFieldNumber = 3;
    private bool hasTaiyangnengCap;
    private int taiyangnengCap_;
    public bool HasTaiyangnengCap {
      get { return hasTaiyangnengCap; }
    }
    public int TaiyangnengCap {
      get { return taiyangnengCap_; }
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
      string[] field_names = _tAIYANGNENGFieldNames;
      if (hasId) {
        output.WriteInt32(1, field_names[0], Id);
      }
      if (hasTaiyangnengSpd) {
        output.WriteInt32(2, field_names[2], TaiyangnengSpd);
      }
      if (hasTaiyangnengCap) {
        output.WriteInt32(3, field_names[1], TaiyangnengCap);
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
        if (hasTaiyangnengSpd) {
          size += pb::CodedOutputStream.ComputeInt32Size(2, TaiyangnengSpd);
        }
        if (hasTaiyangnengCap) {
          size += pb::CodedOutputStream.ComputeInt32Size(3, TaiyangnengCap);
        }
        memoizedSerializedSize = size;
        return size;
      }
    }
    
    #region Lite runtime methods
    public override int GetHashCode() {
      int hash = GetType().GetHashCode();
      if (hasId) hash ^= id_.GetHashCode();
      if (hasTaiyangnengSpd) hash ^= taiyangnengSpd_.GetHashCode();
      if (hasTaiyangnengCap) hash ^= taiyangnengCap_.GetHashCode();
      return hash;
    }
    
    public override bool Equals(object obj) {
      TAIYANGNENG other = obj as TAIYANGNENG;
      if (other == null) return false;
      if (hasId != other.hasId || (hasId && !id_.Equals(other.id_))) return false;
      if (hasTaiyangnengSpd != other.hasTaiyangnengSpd || (hasTaiyangnengSpd && !taiyangnengSpd_.Equals(other.taiyangnengSpd_))) return false;
      if (hasTaiyangnengCap != other.hasTaiyangnengCap || (hasTaiyangnengCap && !taiyangnengCap_.Equals(other.taiyangnengCap_))) return false;
      return true;
    }
    
    #endregion
    
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static TAIYANGNENG ParseFrom(pb::ByteString data) {
      return ((Builder) CreateBuilder().MergeFrom(data)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static TAIYANGNENG ParseFrom(pb::ByteString data, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(data, extensionRegistry)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static TAIYANGNENG ParseFrom(byte[] data) {
      return ((Builder) CreateBuilder().MergeFrom(data)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static TAIYANGNENG ParseFrom(byte[] data, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(data, extensionRegistry)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static TAIYANGNENG ParseFrom(global::System.IO.Stream input) {
      return ((Builder) CreateBuilder().MergeFrom(input)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static TAIYANGNENG ParseFrom(global::System.IO.Stream input, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(input, extensionRegistry)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static TAIYANGNENG ParseDelimitedFrom(global::System.IO.Stream input) {
      return CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static TAIYANGNENG ParseDelimitedFrom(global::System.IO.Stream input, pb::ExtensionRegistry extensionRegistry) {
      return CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static TAIYANGNENG ParseFrom(pb::ICodedInputStream input) {
      return ((Builder) CreateBuilder().MergeFrom(input)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static TAIYANGNENG ParseFrom(pb::ICodedInputStream input, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(input, extensionRegistry)).BuildParsed();
    }
    private TAIYANGNENG MakeReadOnly() {
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
    public static Builder CreateBuilder(TAIYANGNENG prototype) {
      return new Builder(prototype);
    }
    
    public sealed partial class Builder : pb::GeneratedBuilderLite<TAIYANGNENG, Builder> {
      protected override Builder ThisBuilder {
        get { return this; }
      }
      public Builder() {
        result = DefaultInstance;
        resultIsReadOnly = true;
      }
      internal Builder(TAIYANGNENG cloneFrom) {
        result = cloneFrom;
        resultIsReadOnly = true;
      }
      
      private bool resultIsReadOnly;
      private TAIYANGNENG result;
      
      private TAIYANGNENG PrepareBuilder() {
        if (resultIsReadOnly) {
          TAIYANGNENG original = result;
          result = new TAIYANGNENG();
          resultIsReadOnly = false;
          MergeFrom(original);
        }
        return result;
      }
      
      public override bool IsInitialized {
        get { return result.IsInitialized; }
      }
      
      protected override TAIYANGNENG MessageBeingBuilt {
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
      
      public override TAIYANGNENG DefaultInstanceForType {
        get { return global::com.nkm.framework.resource.data.TAIYANGNENG.DefaultInstance; }
      }
      
      public override TAIYANGNENG BuildPartial() {
        if (resultIsReadOnly) {
          return result;
        }
        resultIsReadOnly = true;
        return result.MakeReadOnly();
      }
      
      public override Builder MergeFrom(pb::IMessageLite other) {
        if (other is TAIYANGNENG) {
          return MergeFrom((TAIYANGNENG) other);
        } else {
          base.MergeFrom(other);
          return this;
        }
      }
      
      public override Builder MergeFrom(TAIYANGNENG other) {
        if (other == global::com.nkm.framework.resource.data.TAIYANGNENG.DefaultInstance) return this;
        PrepareBuilder();
        if (other.HasId) {
          Id = other.Id;
        }
        if (other.HasTaiyangnengSpd) {
          TaiyangnengSpd = other.TaiyangnengSpd;
        }
        if (other.HasTaiyangnengCap) {
          TaiyangnengCap = other.TaiyangnengCap;
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
            int field_ordinal = global::System.Array.BinarySearch(_tAIYANGNENGFieldNames, field_name, global::System.StringComparer.Ordinal);
            if(field_ordinal >= 0)
              tag = _tAIYANGNENGFieldTags[field_ordinal];
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
              result.hasTaiyangnengSpd = input.ReadInt32(ref result.taiyangnengSpd_);
              break;
            }
            case 24: {
              result.hasTaiyangnengCap = input.ReadInt32(ref result.taiyangnengCap_);
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
      
      public bool HasTaiyangnengSpd {
        get { return result.hasTaiyangnengSpd; }
      }
      public int TaiyangnengSpd {
        get { return result.TaiyangnengSpd; }
        set { SetTaiyangnengSpd(value); }
      }
      public Builder SetTaiyangnengSpd(int value) {
        PrepareBuilder();
        result.hasTaiyangnengSpd = true;
        result.taiyangnengSpd_ = value;
        return this;
      }
      public Builder ClearTaiyangnengSpd() {
        PrepareBuilder();
        result.hasTaiyangnengSpd = false;
        result.taiyangnengSpd_ = 0;
        return this;
      }
      
      public bool HasTaiyangnengCap {
        get { return result.hasTaiyangnengCap; }
      }
      public int TaiyangnengCap {
        get { return result.TaiyangnengCap; }
        set { SetTaiyangnengCap(value); }
      }
      public Builder SetTaiyangnengCap(int value) {
        PrepareBuilder();
        result.hasTaiyangnengCap = true;
        result.taiyangnengCap_ = value;
        return this;
      }
      public Builder ClearTaiyangnengCap() {
        PrepareBuilder();
        result.hasTaiyangnengCap = false;
        result.taiyangnengCap_ = 0;
        return this;
      }
    }
    static TAIYANGNENG() {
      object.ReferenceEquals(global::com.nkm.framework.resource.data.TaiyangnengBytes.Descriptor, null);
    }
  }
  
  public sealed partial class TAIYANGNENG_ARRAY : pb::GeneratedMessageLite<TAIYANGNENG_ARRAY, TAIYANGNENG_ARRAY.Builder> {
    private TAIYANGNENG_ARRAY() { }
    private static readonly TAIYANGNENG_ARRAY defaultInstance = new TAIYANGNENG_ARRAY().MakeReadOnly();
    private static readonly string[] _tAIYANGNENGARRAYFieldNames = new string[] { "items" };
    private static readonly uint[] _tAIYANGNENGARRAYFieldTags = new uint[] { 10 };
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static TAIYANGNENG_ARRAY DefaultInstance {
      get { return defaultInstance; }
    }
    
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public override TAIYANGNENG_ARRAY DefaultInstanceForType {
      get { return DefaultInstance; }
    }
    
    protected override TAIYANGNENG_ARRAY ThisMessage {
      get { return this; }
    }
    
    #if UNITY_EDITOR
    [pb.FieldNumber]
    #endif//
    public const int ItemsFieldNumber = 1;
    private pbc::PopsicleList<global::com.nkm.framework.resource.data.TAIYANGNENG> items_ = new pbc::PopsicleList<global::com.nkm.framework.resource.data.TAIYANGNENG>();
    public scg::IList<global::com.nkm.framework.resource.data.TAIYANGNENG> ItemsList {
      get { return items_; }
    }
    public int ItemsCount {
      get { return items_.Count; }
    }
    public global::com.nkm.framework.resource.data.TAIYANGNENG GetItems(int index) {
      return items_[index];
    }
    
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public override bool IsInitialized {
      get {
        foreach (global::com.nkm.framework.resource.data.TAIYANGNENG element in ItemsList) {
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
      string[] field_names = _tAIYANGNENGARRAYFieldNames;
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
        foreach (global::com.nkm.framework.resource.data.TAIYANGNENG element in ItemsList) {
          size += pb::CodedOutputStream.ComputeMessageSize(1, element);
        }
        memoizedSerializedSize = size;
        return size;
      }
    }
    
    #region Lite runtime methods
    public override int GetHashCode() {
      int hash = GetType().GetHashCode();
      foreach(global::com.nkm.framework.resource.data.TAIYANGNENG i in items_)
        hash ^= i.GetHashCode();
      return hash;
    }
    
    public override bool Equals(object obj) {
      TAIYANGNENG_ARRAY other = obj as TAIYANGNENG_ARRAY;
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
    public static TAIYANGNENG_ARRAY ParseFrom(pb::ByteString data) {
      return ((Builder) CreateBuilder().MergeFrom(data)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static TAIYANGNENG_ARRAY ParseFrom(pb::ByteString data, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(data, extensionRegistry)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static TAIYANGNENG_ARRAY ParseFrom(byte[] data) {
      return ((Builder) CreateBuilder().MergeFrom(data)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static TAIYANGNENG_ARRAY ParseFrom(byte[] data, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(data, extensionRegistry)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static TAIYANGNENG_ARRAY ParseFrom(global::System.IO.Stream input) {
      return ((Builder) CreateBuilder().MergeFrom(input)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static TAIYANGNENG_ARRAY ParseFrom(global::System.IO.Stream input, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(input, extensionRegistry)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static TAIYANGNENG_ARRAY ParseDelimitedFrom(global::System.IO.Stream input) {
      return CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static TAIYANGNENG_ARRAY ParseDelimitedFrom(global::System.IO.Stream input, pb::ExtensionRegistry extensionRegistry) {
      return CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static TAIYANGNENG_ARRAY ParseFrom(pb::ICodedInputStream input) {
      return ((Builder) CreateBuilder().MergeFrom(input)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static TAIYANGNENG_ARRAY ParseFrom(pb::ICodedInputStream input, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(input, extensionRegistry)).BuildParsed();
    }
    private TAIYANGNENG_ARRAY MakeReadOnly() {
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
    public static Builder CreateBuilder(TAIYANGNENG_ARRAY prototype) {
      return new Builder(prototype);
    }
    
    public sealed partial class Builder : pb::GeneratedBuilderLite<TAIYANGNENG_ARRAY, Builder> {
      protected override Builder ThisBuilder {
        get { return this; }
      }
      public Builder() {
        result = DefaultInstance;
        resultIsReadOnly = true;
      }
      internal Builder(TAIYANGNENG_ARRAY cloneFrom) {
        result = cloneFrom;
        resultIsReadOnly = true;
      }
      
      private bool resultIsReadOnly;
      private TAIYANGNENG_ARRAY result;
      
      private TAIYANGNENG_ARRAY PrepareBuilder() {
        if (resultIsReadOnly) {
          TAIYANGNENG_ARRAY original = result;
          result = new TAIYANGNENG_ARRAY();
          resultIsReadOnly = false;
          MergeFrom(original);
        }
        return result;
      }
      
      public override bool IsInitialized {
        get { return result.IsInitialized; }
      }
      
      protected override TAIYANGNENG_ARRAY MessageBeingBuilt {
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
      
      public override TAIYANGNENG_ARRAY DefaultInstanceForType {
        get { return global::com.nkm.framework.resource.data.TAIYANGNENG_ARRAY.DefaultInstance; }
      }
      
      public override TAIYANGNENG_ARRAY BuildPartial() {
        if (resultIsReadOnly) {
          return result;
        }
        resultIsReadOnly = true;
        return result.MakeReadOnly();
      }
      
      public override Builder MergeFrom(pb::IMessageLite other) {
        if (other is TAIYANGNENG_ARRAY) {
          return MergeFrom((TAIYANGNENG_ARRAY) other);
        } else {
          base.MergeFrom(other);
          return this;
        }
      }
      
      public override Builder MergeFrom(TAIYANGNENG_ARRAY other) {
        if (other == global::com.nkm.framework.resource.data.TAIYANGNENG_ARRAY.DefaultInstance) return this;
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
            int field_ordinal = global::System.Array.BinarySearch(_tAIYANGNENGARRAYFieldNames, field_name, global::System.StringComparer.Ordinal);
            if(field_ordinal >= 0)
              tag = _tAIYANGNENGARRAYFieldTags[field_ordinal];
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
              input.ReadMessageArray(tag, field_name, result.items_, global::com.nkm.framework.resource.data.TAIYANGNENG.DefaultInstance, extensionRegistry);
              break;
            }
          }
        }
        
        return this;
      }
      
      
      public pbc::IPopsicleList<global::com.nkm.framework.resource.data.TAIYANGNENG> ItemsList {
        get { return PrepareBuilder().items_; }
      }
      public int ItemsCount {
        get { return result.ItemsCount; }
      }
      public global::com.nkm.framework.resource.data.TAIYANGNENG GetItems(int index) {
        return result.GetItems(index);
      }
      public Builder SetItems(int index, global::com.nkm.framework.resource.data.TAIYANGNENG value) {
        pb::ThrowHelper.ThrowIfNull(value, "value");
        PrepareBuilder();
        result.items_[index] = value;
        return this;
      }
      public Builder SetItems(int index, global::com.nkm.framework.resource.data.TAIYANGNENG.Builder builderForValue) {
        pb::ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
        PrepareBuilder();
        result.items_[index] = builderForValue.Build();
        return this;
      }
      public Builder AddItems(global::com.nkm.framework.resource.data.TAIYANGNENG value) {
        pb::ThrowHelper.ThrowIfNull(value, "value");
        PrepareBuilder();
        result.items_.Add(value);
        return this;
      }
      public Builder AddItems(global::com.nkm.framework.resource.data.TAIYANGNENG.Builder builderForValue) {
        pb::ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
        PrepareBuilder();
        result.items_.Add(builderForValue.Build());
        return this;
      }
      public Builder AddRangeItems(scg::IEnumerable<global::com.nkm.framework.resource.data.TAIYANGNENG> values) {
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
    static TAIYANGNENG_ARRAY() {
      object.ReferenceEquals(global::com.nkm.framework.resource.data.TaiyangnengBytes.Descriptor, null);
    }
  }
  
  #endregion
  
}

#endregion Designer generated code
#pragma warning restore
