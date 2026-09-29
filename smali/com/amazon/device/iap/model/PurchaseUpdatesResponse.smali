.class public final Lcom/amazon/device/iap/model/PurchaseUpdatesResponse;
.super Ljava/lang/Object;
.source "PurchaseUpdatesResponse.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/amazon/device/iap/model/PurchaseUpdatesResponse$RequestStatus;
    }
.end annotation


# static fields
.field private static final HAS_MORE:Ljava/lang/String; = "HAS_MORE"

.field private static final RECEIPTS:Ljava/lang/String; = "RECEIPTS"

.field private static final REQUEST_ID:Ljava/lang/String; = "REQUEST_ID"

.field private static final REQUEST_STATUS:Ljava/lang/String; = "REQUEST_STATUS"

.field private static final TO_STRING_FORMAT:Ljava/lang/String; = "(%s, requestId: \"%s\", requestStatus: \"%s\", userData: \"%s\", receipts: %s, hasMore: \"%b\")"

.field private static final USER_DATA:Ljava/lang/String; = "USER_DATA"


# instance fields
.field private final hasMore:Z

.field private final receipts:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Lcom/amazon/device/iap/model/Receipt;",
            ">;"
        }
    .end annotation
.end field

.field private final requestId:Lcom/amazon/device/iap/model/RequestId;

.field private final requestStatus:Lcom/amazon/device/iap/model/PurchaseUpdatesResponse$RequestStatus;

.field private final userData:Lcom/amazon/device/iap/model/UserData;


# direct methods
.method public constructor <init>(Lcom/amazon/device/iap/internal/model/PurchaseUpdatesResponseBuilder;)V
    .locals 2

    .line 70
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 72
    invoke-virtual {p1}, Lcom/amazon/device/iap/internal/model/PurchaseUpdatesResponseBuilder;->getRequestId()Lcom/amazon/device/iap/model/RequestId;

    move-result-object v0

    const-string v1, "requestId"

    invoke-static {v0, v1}, Lcom/amazon/device/iap/internal/util/d;->a(Ljava/lang/Object;Ljava/lang/String;)V

    .line 73
    invoke-virtual {p1}, Lcom/amazon/device/iap/internal/model/PurchaseUpdatesResponseBuilder;->getRequestStatus()Lcom/amazon/device/iap/model/PurchaseUpdatesResponse$RequestStatus;

    move-result-object v0

    const-string v1, "requestStatus"

    invoke-static {v0, v1}, Lcom/amazon/device/iap/internal/util/d;->a(Ljava/lang/Object;Ljava/lang/String;)V

    .line 75
    sget-object v0, Lcom/amazon/device/iap/model/PurchaseUpdatesResponse$RequestStatus;->SUCCESSFUL:Lcom/amazon/device/iap/model/PurchaseUpdatesResponse$RequestStatus;

    invoke-virtual {p1}, Lcom/amazon/device/iap/internal/model/PurchaseUpdatesResponseBuilder;->getRequestStatus()Lcom/amazon/device/iap/model/PurchaseUpdatesResponse$RequestStatus;

    move-result-object v1

    if-ne v0, v1, :cond_0

    .line 76
    invoke-virtual {p1}, Lcom/amazon/device/iap/internal/model/PurchaseUpdatesResponseBuilder;->getUserData()Lcom/amazon/device/iap/model/UserData;

    move-result-object v0

    const-string v1, "userData"

    invoke-static {v0, v1}, Lcom/amazon/device/iap/internal/util/d;->a(Ljava/lang/Object;Ljava/lang/String;)V

    .line 77
    invoke-virtual {p1}, Lcom/amazon/device/iap/internal/model/PurchaseUpdatesResponseBuilder;->getReceipts()Ljava/util/List;

    move-result-object v0

    const-string v1, "receipts"

    invoke-static {v0, v1}, Lcom/amazon/device/iap/internal/util/d;->a(Ljava/lang/Object;Ljava/lang/String;)V

    .line 80
    :cond_0
    invoke-virtual {p1}, Lcom/amazon/device/iap/internal/model/PurchaseUpdatesResponseBuilder;->getRequestId()Lcom/amazon/device/iap/model/RequestId;

    move-result-object v0

    iput-object v0, p0, Lcom/amazon/device/iap/model/PurchaseUpdatesResponse;->requestId:Lcom/amazon/device/iap/model/RequestId;

    .line 81
    invoke-virtual {p1}, Lcom/amazon/device/iap/internal/model/PurchaseUpdatesResponseBuilder;->getRequestStatus()Lcom/amazon/device/iap/model/PurchaseUpdatesResponse$RequestStatus;

    move-result-object v0

    iput-object v0, p0, Lcom/amazon/device/iap/model/PurchaseUpdatesResponse;->requestStatus:Lcom/amazon/device/iap/model/PurchaseUpdatesResponse$RequestStatus;

    .line 82
    invoke-virtual {p1}, Lcom/amazon/device/iap/internal/model/PurchaseUpdatesResponseBuilder;->getUserData()Lcom/amazon/device/iap/model/UserData;

    move-result-object v0

    iput-object v0, p0, Lcom/amazon/device/iap/model/PurchaseUpdatesResponse;->userData:Lcom/amazon/device/iap/model/UserData;

    .line 83
    invoke-virtual {p1}, Lcom/amazon/device/iap/internal/model/PurchaseUpdatesResponseBuilder;->getReceipts()Ljava/util/List;

    move-result-object v0

    if-nez v0, :cond_1

    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    goto :goto_0

    :cond_1
    invoke-virtual {p1}, Lcom/amazon/device/iap/internal/model/PurchaseUpdatesResponseBuilder;->getReceipts()Ljava/util/List;

    move-result-object v0

    :goto_0
    iput-object v0, p0, Lcom/amazon/device/iap/model/PurchaseUpdatesResponse;->receipts:Ljava/util/List;

    .line 84
    invoke-virtual {p1}, Lcom/amazon/device/iap/internal/model/PurchaseUpdatesResponseBuilder;->hasMore()Z

    move-result p1

    iput-boolean p1, p0, Lcom/amazon/device/iap/model/PurchaseUpdatesResponse;->hasMore:Z

    return-void
.end method


# virtual methods
.method public getReceipts()Ljava/util/List;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "Lcom/amazon/device/iap/model/Receipt;",
            ">;"
        }
    .end annotation

    .line 129
    iget-object v0, p0, Lcom/amazon/device/iap/model/PurchaseUpdatesResponse;->receipts:Ljava/util/List;

    return-object v0
.end method

.method public getRequestId()Lcom/amazon/device/iap/model/RequestId;
    .locals 1

    .line 96
    iget-object v0, p0, Lcom/amazon/device/iap/model/PurchaseUpdatesResponse;->requestId:Lcom/amazon/device/iap/model/RequestId;

    return-object v0
.end method

.method public getRequestStatus()Lcom/amazon/device/iap/model/PurchaseUpdatesResponse$RequestStatus;
    .locals 1

    .line 115
    iget-object v0, p0, Lcom/amazon/device/iap/model/PurchaseUpdatesResponse;->requestStatus:Lcom/amazon/device/iap/model/PurchaseUpdatesResponse$RequestStatus;

    return-object v0
.end method

.method public getUserData()Lcom/amazon/device/iap/model/UserData;
    .locals 1

    .line 106
    iget-object v0, p0, Lcom/amazon/device/iap/model/PurchaseUpdatesResponse;->userData:Lcom/amazon/device/iap/model/UserData;

    return-object v0
.end method

.method public hasMore()Z
    .locals 1

    .line 140
    iget-boolean v0, p0, Lcom/amazon/device/iap/model/PurchaseUpdatesResponse;->hasMore:Z

    return v0
.end method

.method public toJSON()Lorg/json/JSONObject;
    .locals 4
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/json/JSONException;
        }
    .end annotation

    .line 167
    new-instance v0, Lorg/json/JSONObject;

    invoke-direct {v0}, Lorg/json/JSONObject;-><init>()V

    const-string v1, "REQUEST_ID"

    .line 168
    iget-object v2, p0, Lcom/amazon/device/iap/model/PurchaseUpdatesResponse;->requestId:Lcom/amazon/device/iap/model/RequestId;

    invoke-virtual {v0, v1, v2}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    const-string v1, "REQUEST_STATUS"

    .line 169
    iget-object v2, p0, Lcom/amazon/device/iap/model/PurchaseUpdatesResponse;->requestStatus:Lcom/amazon/device/iap/model/PurchaseUpdatesResponse$RequestStatus;

    invoke-virtual {v0, v1, v2}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    const-string v1, "USER_DATA"

    .line 170
    iget-object v2, p0, Lcom/amazon/device/iap/model/PurchaseUpdatesResponse;->userData:Lcom/amazon/device/iap/model/UserData;

    if-eqz v2, :cond_0

    iget-object v2, p0, Lcom/amazon/device/iap/model/PurchaseUpdatesResponse;->userData:Lcom/amazon/device/iap/model/UserData;

    invoke-virtual {v2}, Lcom/amazon/device/iap/model/UserData;->toJSON()Lorg/json/JSONObject;

    move-result-object v2

    goto :goto_0

    :cond_0
    const-string v2, ""

    :goto_0
    invoke-virtual {v0, v1, v2}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    .line 171
    new-instance v1, Lorg/json/JSONArray;

    invoke-direct {v1}, Lorg/json/JSONArray;-><init>()V

    .line 172
    iget-object v2, p0, Lcom/amazon/device/iap/model/PurchaseUpdatesResponse;->receipts:Ljava/util/List;

    if-eqz v2, :cond_1

    .line 173
    iget-object v2, p0, Lcom/amazon/device/iap/model/PurchaseUpdatesResponse;->receipts:Ljava/util/List;

    invoke-interface {v2}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v2

    :goto_1
    invoke-interface {v2}, Ljava/util/Iterator;->hasNext()Z

    move-result v3

    if-eqz v3, :cond_1

    invoke-interface {v2}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Lcom/amazon/device/iap/model/Receipt;

    .line 174
    invoke-virtual {v3}, Lcom/amazon/device/iap/model/Receipt;->toJSON()Lorg/json/JSONObject;

    move-result-object v3

    invoke-virtual {v1, v3}, Lorg/json/JSONArray;->put(Ljava/lang/Object;)Lorg/json/JSONArray;

    goto :goto_1

    :cond_1
    const-string v2, "RECEIPTS"

    .line 177
    invoke-virtual {v0, v2, v1}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    const-string v1, "HAS_MORE"

    .line 178
    iget-boolean v2, p0, Lcom/amazon/device/iap/model/PurchaseUpdatesResponse;->hasMore:Z

    invoke-virtual {v0, v1, v2}, Lorg/json/JSONObject;->put(Ljava/lang/String;Z)Lorg/json/JSONObject;

    return-object v0
.end method

.method public toString()Ljava/lang/String;
    .locals 4

    const-string v0, "(%s, requestId: \"%s\", requestStatus: \"%s\", userData: \"%s\", receipts: %s, hasMore: \"%b\")"

    const/4 v1, 0x6

    .line 150
    new-array v1, v1, [Ljava/lang/Object;

    invoke-super {p0}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v2

    const/4 v3, 0x0

    aput-object v2, v1, v3

    iget-object v2, p0, Lcom/amazon/device/iap/model/PurchaseUpdatesResponse;->requestId:Lcom/amazon/device/iap/model/RequestId;

    const/4 v3, 0x1

    aput-object v2, v1, v3

    iget-object v2, p0, Lcom/amazon/device/iap/model/PurchaseUpdatesResponse;->requestStatus:Lcom/amazon/device/iap/model/PurchaseUpdatesResponse$RequestStatus;

    const/4 v3, 0x2

    aput-object v2, v1, v3

    iget-object v2, p0, Lcom/amazon/device/iap/model/PurchaseUpdatesResponse;->userData:Lcom/amazon/device/iap/model/UserData;

    const/4 v3, 0x3

    aput-object v2, v1, v3

    iget-object v2, p0, Lcom/amazon/device/iap/model/PurchaseUpdatesResponse;->receipts:Ljava/util/List;

    if-eqz v2, :cond_0

    iget-object v2, p0, Lcom/amazon/device/iap/model/PurchaseUpdatesResponse;->receipts:Ljava/util/List;

    invoke-interface {v2}, Ljava/util/List;->toArray()[Ljava/lang/Object;

    move-result-object v2

    invoke-static {v2}, Ljava/util/Arrays;->toString([Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v2

    goto :goto_0

    :cond_0
    const-string v2, "null"

    :goto_0
    const/4 v3, 0x4

    aput-object v2, v1, v3

    const/4 v2, 0x5

    iget-boolean v3, p0, Lcom/amazon/device/iap/model/PurchaseUpdatesResponse;->hasMore:Z

    invoke-static {v3}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v3

    aput-object v3, v1, v2

    invoke-static {v0, v1}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method
