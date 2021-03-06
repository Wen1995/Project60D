// Generated by ProtoGen, Version=2.4.1.521, Culture=neutral, PublicKeyToken=null.  DO NOT EDIT!
#pragma warning disable
#region Designer generated code

using pb = global::Google.ProtocolBuffers;
using pbc = global::Google.ProtocolBuffers.Collections;
using pbd = global::Google.ProtocolBuffers.Descriptors;
using scg = global::System.Collections.Generic;
namespace com.nkm.framework.resource.data {
  
  public static partial class JsfadianjiBytes {
  
    #region Extension registration
    public static void RegisterAllExtensions(pb::ExtensionRegistry registry) {
    }
    #endregion
    #region Static variables
    #endregion
    #region Extensions
    internal static readonly object Descriptor;
    static JsfadianjiBytes() {
      Descriptor = null;
    }
    #endregion
    
  }
  #region Messages
  public sealed partial class JSFADIANJI : pb::GeneratedMessageLite<JSFADIANJI, JSFADIANJI.Builder> {
    private JSFADIANJI() { }
    private static readonly JSFADIANJI defaultInstance = new JSFADIANJI().MakeReadOnly();
    private static readonly string[] _jSFADIANJIFieldNames = new string[] { "id", "jsfadianji_cap", "jsfadianji_spd" };
    private static readonly uint[] _jSFADIANJIFieldTags = new uint[] { 8, 24, 16 };
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static JSFADIANJI DefaultInstance {
      get { return defaultInstance; }
    }
    
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public override JSFADIANJI DefaultInstanceForType {
      get { return DefaultInstance; }
    }
    
    protected override JSFADIANJI ThisMessage {
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
    public const int JsfadianjiSpdFieldNumber = 2;
    private bool hasJsfadianjiSpd;
    private int jsfadianjiSpd_;
    public bool HasJsfadianjiSpd {
      get { return hasJsfadianjiSpd; }
    }
    public int JsfadianjiSpd {
      get { return jsfadianjiSpd_; }
    }
    
    #if UNITY_EDITOR
    [pb.FieldNumber]
    #endif//
    public const int JsfadianjiCapFieldNumber = 3;
    private bool hasJsfadianjiCap;
    private int jsfadianjiCap_;
    public bool HasJsfadianjiCap {
      get { return hasJsfadianjiCap; }
    }
    public int JsfadianjiCap {
      get { return jsfadianjiCap_; }
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
      string[] field_names = _jSFADIANJIFieldNames;
      if (hasId) {
        output.WriteInt32(1, field_names[0], Id);
      }
      if (hasJsfadianjiSpd) {
        output.WriteInt32(2, field_names[2], JsfadianjiSpd);
      }
      if (hasJsfadianjiCap) {
        output.WriteInt32(3, field_names[1], JsfadianjiCap);
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
        if (hasJsfadianjiSpd) {
          size += pb::CodedOutputStream.ComputeInt32Size(2, JsfadianjiSpd);
        }
        if (hasJsfadianjiCap) {
          size += pb::CodedOutputStream.ComputeInt32Size(3, JsfadianjiCap);
        }
        memoizedSerializedSize = size;
        return size;
      }
    }
    
    #region Lite runtime methods
    public override int GetHashCode() {
      int hash = GetType().GetHashCode();
      if (hasId) hash ^= id_.GetHashCode();
      if (hasJsfadianjiSpd) hash ^= jsfadianjiSpd_.GetHashCode();
      if (hasJsfadianjiCap) hash ^= jsfadianjiCap_.GetHashCode();
      return hash;
    }
    
    public override bool Equals(object obj) {
      JSFADIANJI other = obj as JSFADIANJI;
      if (other == null) return false;
      if (hasId != other.hasId || (hasId && !id_.Equals(other.id_))) return false;
      if (hasJsfadianjiSpd != other.hasJsfadianjiSpd || (hasJsfadianjiSpd && !jsfadianjiSpd_.Equals(other.jsfadianjiSpd_))) return false;
      if (hasJsfadianjiCap != other.hasJsfadianjiCap || (hasJsfadianjiCap && !jsfadianjiCap_.Equals(other.jsfadianjiCap_))) return false;
      return true;
    }
    
    #endregion
    
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static JSFADIANJI ParseFrom(pb::ByteString data) {
      return ((Builder) CreateBuilder().MergeFrom(data)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static JSFADIANJI ParseFrom(pb::ByteString data, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(data, extensionRegistry)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static JSFADIANJI ParseFrom(byte[] data) {
      return ((Builder) CreateBuilder().MergeFrom(data)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static JSFADIANJI ParseFrom(byte[] data, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(data, extensionRegistry)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static JSFADIANJI ParseFrom(global::System.IO.Stream input) {
      return ((Builder) CreateBuilder().MergeFrom(input)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static JSFADIANJI ParseFrom(global::System.IO.Stream input, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(input, extensionRegistry)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static JSFADIANJI ParseDelimitedFrom(global::System.IO.Stream input) {
      return CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static JSFADIANJI ParseDelimitedFrom(global::System.IO.Stream input, pb::ExtensionRegistry extensionRegistry) {
      return CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static JSFADIANJI ParseFrom(pb::ICodedInputStream input) {
      return ((Builder) CreateBuilder().MergeFrom(input)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static JSFADIANJI ParseFrom(pb::ICodedInputStream input, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(input, extensionRegistry)).BuildParsed();
    }
    private JSFADIANJI MakeReadOnly() {
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
    public static Builder CreateBuilder(JSFADIANJI prototype) {
      return new Builder(prototype);
    }
    
    public sealed partial class Builder : pb::GeneratedBuilderLite<JSFADIANJI, Builder> {
      protected override Builder ThisBuilder {
        get { return this; }
      }
      public Builder() {
        result = DefaultInstance;
        resultIsReadOnly = true;
      }
      internal Builder(JSFADIANJI cloneFrom) {
        result = cloneFrom;
        resultIsReadOnly = true;
      }
      
      private bool resultIsReadOnly;
      private JSFADIANJI result;
      
      private JSFADIANJI PrepareBuilder() {
        if (resultIsReadOnly) {
          JSFADIANJI original = result;
          result = new JSFADIANJI();
          resultIsReadOnly = false;
          MergeFrom(original);
        }
        return result;
      }
      
      public override bool IsInitialized {
        get { return result.IsInitialized; }
      }
      
      protected override JSFADIANJI MessageBeingBuilt {
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
      
      public override JSFADIANJI DefaultInstanceForType {
        get { return global::com.nkm.framework.resource.data.JSFADIANJI.DefaultInstance; }
      }
      
      public override JSFADIANJI BuildPartial() {
        if (resultIsReadOnly) {
          return result;
        }
        resultIsReadOnly = true;
        return result.MakeReadOnly();
      }
      
      public override Builder MergeFrom(pb::IMessageLite other) {
        if (other is JSFADIANJI) {
          return MergeFrom((JSFADIANJI) other);
        } else {
          base.MergeFrom(other);
          return this;
        }
      }
      
      public override Builder MergeFrom(JSFADIANJI other) {
        if (other == global::com.nkm.framework.resource.data.JSFADIANJI.DefaultInstance) return this;
        PrepareBuilder();
        if (other.HasId) {
          Id = other.Id;
        }
        if (other.HasJsfadianjiSpd) {
          JsfadianjiSpd = other.JsfadianjiSpd;
        }
        if (other.HasJsfadianjiCap) {
          JsfadianjiCap = other.JsfadianjiCap;
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
            int field_ordinal = global::System.Array.BinarySearch(_jSFADIANJIFieldNames, field_name, global::System.StringComparer.Ordinal);
            if(field_ordinal >= 0)
              tag = _jSFADIANJIFieldTags[field_ordinal];
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
              result.hasJsfadianjiSpd = input.ReadInt32(ref result.jsfadianjiSpd_);
              break;
            }
            case 24: {
              result.hasJsfadianjiCap = input.ReadInt32(ref result.jsfadianjiCap_);
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
      
      public bool HasJsfadianjiSpd {
        get { return result.hasJsfadianjiSpd; }
      }
      public int JsfadianjiSpd {
        get { return result.JsfadianjiSpd; }
        set { SetJsfadianjiSpd(value); }
      }
      public Builder SetJsfadianjiSpd(int value) {
        PrepareBuilder();
        result.hasJsfadianjiSpd = true;
        result.jsfadianjiSpd_ = value;
        return this;
      }
      public Builder ClearJsfadianjiSpd() {
        PrepareBuilder();
        result.hasJsfadianjiSpd = false;
        result.jsfadianjiSpd_ = 0;
        return this;
      }
      
      public bool HasJsfadianjiCap {
        get { return result.hasJsfadianjiCap; }
      }
      public int JsfadianjiCap {
        get { return result.JsfadianjiCap; }
        set { SetJsfadianjiCap(value); }
      }
      public Builder SetJsfadianjiCap(int value) {
        PrepareBuilder();
        result.hasJsfadianjiCap = true;
        result.jsfadianjiCap_ = value;
        return this;
      }
      public Builder ClearJsfadianjiCap() {
        PrepareBuilder();
        result.hasJsfadianjiCap = false;
        result.jsfadianjiCap_ = 0;
        return this;
      }
    }
    static JSFADIANJI() {
      object.ReferenceEquals(global::com.nkm.framework.resource.data.JsfadianjiBytes.Descriptor, null);
    }
  }
  
  public sealed partial class JSFADIANJI_ARRAY : pb::GeneratedMessageLite<JSFADIANJI_ARRAY, JSFADIANJI_ARRAY.Builder> {
    private JSFADIANJI_ARRAY() { }
    private static readonly JSFADIANJI_ARRAY defaultInstance = new JSFADIANJI_ARRAY().MakeReadOnly();
    private static readonly string[] _jSFADIANJIARRAYFieldNames = new string[] { "items" };
    private static readonly uint[] _jSFADIANJIARRAYFieldTags = new uint[] { 10 };
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static JSFADIANJI_ARRAY DefaultInstance {
      get { return defaultInstance; }
    }
    
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public override JSFADIANJI_ARRAY DefaultInstanceForType {
      get { return DefaultInstance; }
    }
    
    protected override JSFADIANJI_ARRAY ThisMessage {
      get { return this; }
    }
    
    #if UNITY_EDITOR
    [pb.FieldNumber]
    #endif//
    public const int ItemsFieldNumber = 1;
    private pbc::PopsicleList<global::com.nkm.framework.resource.data.JSFADIANJI> items_ = new pbc::PopsicleList<global::com.nkm.framework.resource.data.JSFADIANJI>();
    public scg::IList<global::com.nkm.framework.resource.data.JSFADIANJI> ItemsList {
      get { return items_; }
    }
    public int ItemsCount {
      get { return items_.Count; }
    }
    public global::com.nkm.framework.resource.data.JSFADIANJI GetItems(int index) {
      return items_[index];
    }
    
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public override bool IsInitialized {
      get {
        foreach (global::com.nkm.framework.resource.data.JSFADIANJI element in ItemsList) {
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
      string[] field_names = _jSFADIANJIARRAYFieldNames;
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
        foreach (global::com.nkm.framework.resource.data.JSFADIANJI element in ItemsList) {
          size += pb::CodedOutputStream.ComputeMessageSize(1, element);
        }
        memoizedSerializedSize = size;
        return size;
      }
    }
    
    #region Lite runtime methods
    public override int GetHashCode() {
      int hash = GetType().GetHashCode();
      foreach(global::com.nkm.framework.resource.data.JSFADIANJI i in items_)
        hash ^= i.GetHashCode();
      return hash;
    }
    
    public override bool Equals(object obj) {
      JSFADIANJI_ARRAY other = obj as JSFADIANJI_ARRAY;
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
    public static JSFADIANJI_ARRAY ParseFrom(pb::ByteString data) {
      return ((Builder) CreateBuilder().MergeFrom(data)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static JSFADIANJI_ARRAY ParseFrom(pb::ByteString data, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(data, extensionRegistry)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static JSFADIANJI_ARRAY ParseFrom(byte[] data) {
      return ((Builder) CreateBuilder().MergeFrom(data)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static JSFADIANJI_ARRAY ParseFrom(byte[] data, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(data, extensionRegistry)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static JSFADIANJI_ARRAY ParseFrom(global::System.IO.Stream input) {
      return ((Builder) CreateBuilder().MergeFrom(input)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static JSFADIANJI_ARRAY ParseFrom(global::System.IO.Stream input, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(input, extensionRegistry)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static JSFADIANJI_ARRAY ParseDelimitedFrom(global::System.IO.Stream input) {
      return CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static JSFADIANJI_ARRAY ParseDelimitedFrom(global::System.IO.Stream input, pb::ExtensionRegistry extensionRegistry) {
      return CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static JSFADIANJI_ARRAY ParseFrom(pb::ICodedInputStream input) {
      return ((Builder) CreateBuilder().MergeFrom(input)).BuildParsed();
    }
    #if UNITY_EDITOR
     [pb.FieldNumber] 
     #endif//
    public static JSFADIANJI_ARRAY ParseFrom(pb::ICodedInputStream input, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(input, extensionRegistry)).BuildParsed();
    }
    private JSFADIANJI_ARRAY MakeReadOnly() {
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
    public static Builder CreateBuilder(JSFADIANJI_ARRAY prototype) {
      return new Builder(prototype);
    }
    
    public sealed partial class Builder : pb::GeneratedBuilderLite<JSFADIANJI_ARRAY, Builder> {
      protected override Builder ThisBuilder {
        get { return this; }
      }
      public Builder() {
        result = DefaultInstance;
        resultIsReadOnly = true;
      }
      internal Builder(JSFADIANJI_ARRAY cloneFrom) {
        result = cloneFrom;
        resultIsReadOnly = true;
      }
      
      private bool resultIsReadOnly;
      private JSFADIANJI_ARRAY result;
      
      private JSFADIANJI_ARRAY PrepareBuilder() {
        if (resultIsReadOnly) {
          JSFADIANJI_ARRAY original = result;
          result = new JSFADIANJI_ARRAY();
          resultIsReadOnly = false;
          MergeFrom(original);
        }
        return result;
      }
      
      public override bool IsInitialized {
        get { return result.IsInitialized; }
      }
      
      protected override JSFADIANJI_ARRAY MessageBeingBuilt {
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
      
      public override JSFADIANJI_ARRAY DefaultInstanceForType {
        get { return global::com.nkm.framework.resource.data.JSFADIANJI_ARRAY.DefaultInstance; }
      }
      
      public override JSFADIANJI_ARRAY BuildPartial() {
        if (resultIsReadOnly) {
          return result;
        }
        resultIsReadOnly = true;
        return result.MakeReadOnly();
      }
      
      public override Builder MergeFrom(pb::IMessageLite other) {
        if (other is JSFADIANJI_ARRAY) {
          return MergeFrom((JSFADIANJI_ARRAY) other);
        } else {
          base.MergeFrom(other);
          return this;
        }
      }
      
      public override Builder MergeFrom(JSFADIANJI_ARRAY other) {
        if (other == global::com.nkm.framework.resource.data.JSFADIANJI_ARRAY.DefaultInstance) return this;
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
            int field_ordinal = global::System.Array.BinarySearch(_jSFADIANJIARRAYFieldNames, field_name, global::System.StringComparer.Ordinal);
            if(field_ordinal >= 0)
              tag = _jSFADIANJIARRAYFieldTags[field_ordinal];
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
              input.ReadMessageArray(tag, field_name, result.items_, global::com.nkm.framework.resource.data.JSFADIANJI.DefaultInstance, extensionRegistry);
              break;
            }
          }
        }
        
        return this;
      }
      
      
      public pbc::IPopsicleList<global::com.nkm.framework.resource.data.JSFADIANJI> ItemsList {
        get { return PrepareBuilder().items_; }
      }
      public int ItemsCount {
        get { return result.ItemsCount; }
      }
      public global::com.nkm.framework.resource.data.JSFADIANJI GetItems(int index) {
        return result.GetItems(index);
      }
      public Builder SetItems(int index, global::com.nkm.framework.resource.data.JSFADIANJI value) {
        pb::ThrowHelper.ThrowIfNull(value, "value");
        PrepareBuilder();
        result.items_[index] = value;
        return this;
      }
      public Builder SetItems(int index, global::com.nkm.framework.resource.data.JSFADIANJI.Builder builderForValue) {
        pb::ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
        PrepareBuilder();
        result.items_[index] = builderForValue.Build();
        return this;
      }
      public Builder AddItems(global::com.nkm.framework.resource.data.JSFADIANJI value) {
        pb::ThrowHelper.ThrowIfNull(value, "value");
        PrepareBuilder();
        result.items_.Add(value);
        return this;
      }
      public Builder AddItems(global::com.nkm.framework.resource.data.JSFADIANJI.Builder builderForValue) {
        pb::ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
        PrepareBuilder();
        result.items_.Add(builderForValue.Build());
        return this;
      }
      public Builder AddRangeItems(scg::IEnumerable<global::com.nkm.framework.resource.data.JSFADIANJI> values) {
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
    static JSFADIANJI_ARRAY() {
      object.ReferenceEquals(global::com.nkm.framework.resource.data.JsfadianjiBytes.Descriptor, null);
    }
  }
  
  #endregion
  
}

#endregion Designer generated code
#pragma warning restore
