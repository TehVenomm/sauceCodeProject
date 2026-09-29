.class public Lnet/gogame/gowrap/support/LocaleDescriptor;
.super Ljava/lang/Object;
.source "LocaleDescriptor.java"


# instance fields
.field private final id:Ljava/lang/String;

.field private final name:Ljava/lang/String;


# direct methods
.method public constructor <init>(Ljava/lang/String;Ljava/lang/String;)V
    .locals 0

    .line 9
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 11
    iput-object p1, p0, Lnet/gogame/gowrap/support/LocaleDescriptor;->id:Ljava/lang/String;

    .line 12
    iput-object p2, p0, Lnet/gogame/gowrap/support/LocaleDescriptor;->name:Ljava/lang/String;

    return-void
.end method


# virtual methods
.method public getId()Ljava/lang/String;
    .locals 1

    .line 16
    iget-object v0, p0, Lnet/gogame/gowrap/support/LocaleDescriptor;->id:Ljava/lang/String;

    return-object v0
.end method

.method public getName()Ljava/lang/String;
    .locals 1

    .line 20
    iget-object v0, p0, Lnet/gogame/gowrap/support/LocaleDescriptor;->name:Ljava/lang/String;

    return-object v0
.end method

.method public toString()Ljava/lang/String;
    .locals 1

    .line 25
    iget-object v0, p0, Lnet/gogame/gowrap/support/LocaleDescriptor;->name:Ljava/lang/String;

    return-object v0
.end method
