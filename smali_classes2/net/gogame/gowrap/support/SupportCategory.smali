.class public Lnet/gogame/gowrap/support/SupportCategory;
.super Ljava/lang/Object;
.source "SupportCategory.java"


# instance fields
.field private final id:Ljava/lang/String;

.field private final stringResourceId:I


# direct methods
.method public constructor <init>(Ljava/lang/String;I)V
    .locals 0

    .line 9
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 10
    iput-object p1, p0, Lnet/gogame/gowrap/support/SupportCategory;->id:Ljava/lang/String;

    .line 11
    iput p2, p0, Lnet/gogame/gowrap/support/SupportCategory;->stringResourceId:I

    return-void
.end method


# virtual methods
.method public getId()Ljava/lang/String;
    .locals 1

    .line 15
    iget-object v0, p0, Lnet/gogame/gowrap/support/SupportCategory;->id:Ljava/lang/String;

    return-object v0
.end method

.method public getStringResourceId()I
    .locals 1

    .line 19
    iget v0, p0, Lnet/gogame/gowrap/support/SupportCategory;->stringResourceId:I

    return v0
.end method
