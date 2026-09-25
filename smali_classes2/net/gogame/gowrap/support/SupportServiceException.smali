.class public Lnet/gogame/gowrap/support/SupportServiceException;
.super Ljava/lang/Exception;
.source "SupportServiceException.java"


# instance fields
.field private final code:Ljava/lang/Integer;


# direct methods
.method public constructor <init>(ILjava/lang/String;)V
    .locals 0

    .line 14
    invoke-direct {p0, p2}, Ljava/lang/Exception;-><init>(Ljava/lang/String;)V

    .line 16
    invoke-static {p1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p1

    iput-object p1, p0, Lnet/gogame/gowrap/support/SupportServiceException;->code:Ljava/lang/Integer;

    return-void
.end method

.method public constructor <init>(Ljava/lang/Throwable;)V
    .locals 0

    .line 8
    invoke-direct {p0, p1}, Ljava/lang/Exception;-><init>(Ljava/lang/Throwable;)V

    const/4 p1, 0x0

    .line 10
    iput-object p1, p0, Lnet/gogame/gowrap/support/SupportServiceException;->code:Ljava/lang/Integer;

    return-void
.end method


# virtual methods
.method public getCode()Ljava/lang/Integer;
    .locals 1

    .line 20
    iget-object v0, p0, Lnet/gogame/gowrap/support/SupportServiceException;->code:Ljava/lang/Integer;

    return-object v0
.end method
